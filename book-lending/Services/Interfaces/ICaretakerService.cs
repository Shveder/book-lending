using book_lending.DTO;
using book_lending.Models;

namespace book_lending.Services.Interfaces;

public interface ICaretakerService
{
    Task AddBook(AddBookRequest request);
    Task<IQueryable<Book>> GetAllBooks();
    Task DeleteBook(DeleteBookRequest request);
}