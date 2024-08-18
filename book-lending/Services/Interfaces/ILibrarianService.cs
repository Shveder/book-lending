namespace book_lending.Services.Interfaces;

public interface ILibrarianService
{
    Task GiveBook(GiveBookRequest request);
    Task ReturnBook(Guid userId, Guid bookOwnId, string status);
}