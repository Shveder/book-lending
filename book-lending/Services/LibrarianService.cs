using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;

namespace book_lending.Services;

public class LibrarianService : ILibrarianService
{
    private readonly IDbRepository _repository;
    private readonly IGetModelService _modelService;

    public LibrarianService(IDbRepository repository, IGetModelService modelService)
    {
        _repository = repository;
        _modelService = modelService;
    }

    public async Task GiveBook(GiveBookRequest request)
    {
        const string requestOperation = "GiveBook";

        if (!(await _modelService.IsUserHasPermission(request.UserId, requestOperation)))
            throw new IncorrectDataException($"User does not have permission to operation ({requestOperation})");
        if (!(await DidUserReturnBooks(request.UserId)))
            throw new IncorrectDataException("User doesn't return all expired books");
        if (!_modelService.IsBookAvailable(request.BookId))
            throw new IncorrectDataException("Book is not available");
        
        var book = _modelService.GetBookById(request.BookId);
        if (book.Status != "Factory new")
            throw new IncorrectDataException($"Book {book.Name} is damaged, it cant be provided to user");

        var bookOwn = new BookOwnership()
        {
            User = _modelService.GetUserById(request.UserId),
            Book = _modelService.GetBookById(request.BookId),
            ReturnDate = DateTime.UtcNow.AddDays(21)
        };

        await _repository.Add(bookOwn);
        await _repository.SaveChangesAsync();
    }

    private async Task<bool> DidUserReturnBooks(Guid userId)
    {
        var userBooks = await _modelService.GetUserBooks(userId);
        return userBooks.All(userBook => userBook.ReturnDate >= DateTime.UtcNow);
    }

    public async Task ReturnBook(Guid bookOwnId, string status)
    {
        var bookOwn = _modelService.GetBookOwnership(bookOwnId);
        var book = _modelService.GetBookById(bookOwn.Book.Id);

        book.Status = status;
        book.DateUpdated = DateTime.UtcNow;
        
        await _repository.Delete<BookOwnership>(bookOwn.Id);
        await _repository.Update(book);
        await _repository.SaveChangesAsync();
    }
}