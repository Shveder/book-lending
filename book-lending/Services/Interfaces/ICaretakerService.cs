namespace book_lending.Services.Interfaces;

public interface ICaretakerService
{
    Task AddBook(AddBookRequest request);
    Task<IQueryable<Book>> GetAllBooks();
    Task DeleteBook(DeleteBookRequest request);
}