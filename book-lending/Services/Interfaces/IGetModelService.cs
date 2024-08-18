using book_lending.Models;

namespace book_lending.Services.Interfaces;

public interface IGetModelService
{
    Operation GetOperationById(Guid id);
    Role GetRoleById(Guid id);
    UserModel GetUserById(Guid id);
    Task<List<UserRole>> GetUserRoles(Guid userId);
    Task<List<RoleOperation>> GetRoleOperations(Guid userId);
}