using book_lending.Models;

namespace book_lending.Services.Interfaces;

public interface IGetModelService
{
    Operation GetOperationById(Guid id);
    Role GetRoleById(Guid id);
    UserModel GetUserById(Guid id);
    Task<List<UserRole>> GetUserRoles(Guid userId);
    Task<List<RoleOperation>> GetRoleOperations(Guid userId);
    Task<List<BookOwnership>> GetUserBooks(Guid userId);
    Task<bool> IsUserHasPermission(Guid userId, string requestOperation);
    Book GetBookById(Guid bookId);
    bool IsBookAvailable(Guid bookId);
    BookOwnership GetBookOwnership(Guid bookId);
    Task<IQueryable<Book>> GetAllBooks();
    Task<IQueryable<UserModel>> GetAllUsers();
    Task<IQueryable<BookOwnership>> GetAllOwnerships();
}