using book_lending.DTO;

namespace book_lending.Services.Interfaces;

public interface IAdminService
{
    Task AddNewRole(AddRoleRequest request);
}