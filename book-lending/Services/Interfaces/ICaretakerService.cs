namespace book_lending.Services.Interfaces;

public interface ICaretakerService
{
    Task AddBook(AddBookRequest request);
    Task DeleteBook(DeleteBookRequest request);
    
}