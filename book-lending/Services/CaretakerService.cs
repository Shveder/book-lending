using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;

namespace book_lending.Services;

public class CaretakerService : ICaretakerService
{
    private readonly IDbRepository _repository;
    private readonly IGetModelService _modelService;

    public CaretakerService(IDbRepository repository, IGetModelService modelService)
    {
        _repository = repository;
        _modelService = modelService;
    }

    public async Task AddBook(AddBookRequest request)
    {
        const string requestOperation = "AddBook";


        if (!(await _modelService.IsUserHasPermission(request.UserId, requestOperation)))
            throw new IncorrectDataException($"User does not have permission to operation ({requestOperation})");
        
        var book = new Book()
        {
            Name = request.Name,
            Status = "Factory new"
        };
        await _repository.Add(book);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteBook(DeleteBookRequest request)
    {
        var book = _modelService.GetBookById(request.BookId);
        const string requestOperation = "DeleteDamagedBook";
        
        if (!(await _modelService.IsUserHasPermission(request.UserId, requestOperation)))
            throw new IncorrectDataException($"User does not have permission to operation ({requestOperation})");
        
        if (book.Status != "Cant be repaired")
            throw new IncorrectDataException("This book is not critically damaged");

        await _repository.Delete<Book>(request.BookId);
        await _repository.SaveChangesAsync();
    }
}