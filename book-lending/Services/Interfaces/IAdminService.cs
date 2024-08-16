using book_lending.DTO;
using book_lending.Models;

namespace book_lending.Services.Interfaces;

public interface IAdminService
{
    Task AddNewRole(string role);
    Task AddNewOperation(string operation);
    Task<IQueryable<Role>> GetRoles();
    Task<IQueryable<Operation>> GetOperations();
    Task AddOperationToRole(AddOperationToRoleRequest request);
}