using book_lending.DTO;

namespace book_lending.Services.Interfaces;

public interface ICaretakerService
{
    Task AddBook(AddBookRequest request);
}