using book_lending.DTO;
using book_lending.Exceptions;
using book_lending.Models;
using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace book_lending.Services;

public class AdminService : IAdminService
{
    private readonly ILogger<AuthorizationService> _logger;
    private readonly IDbRepository _repository;

    public AdminService(IDbRepository repository, ILogger<AuthorizationService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task AddNewRole(string roleName)
    {
        if (await IsRoleUnique(roleName))
            throw new IncorrectDataException("There is already a role with this name in the system");

        var role = new Role()
        {
            RoleName = roleName
        };

        await _repository.Add(role);
        await _repository.SaveChangesAsync();
        _logger.LogInformation($"New role {roleName} is added");
    }

    private async Task<bool> IsRoleUnique(string name)
    {
        var role = await _repository.Get<Role>(model => model.RoleName == name).FirstOrDefaultAsync();
        return role != null;
    }

    public async Task AddNewOperation(string operationName)
    {
        if (await IsOperationUnique(operationName))
            throw new IncorrectDataException("There is already a operation with this name in the system");

        var operation = new Operation()
        {
            OperationName = operationName
        };

        await _repository.Add(operation);
        await _repository.SaveChangesAsync();
        _logger.LogInformation($"New operation {operationName} is added");
    }

    public async Task<IQueryable<Role>> GetRoles()
    {
        return await Task.FromResult(_repository.GetAll<Role>());
    }

    public async Task<IQueryable<Operation>> GetOperations()
    {
        return await Task.FromResult(_repository.GetAll<Operation>());
    }

    private async Task<bool> IsOperationUnique(string name)
    {
        var operation = await _repository.Get<Operation>(model => model.OperationName == name).FirstOrDefaultAsync();
        return operation != null;
    }

    public async Task AddOperationToRole(AddOperationToRoleRequest request)
    {
        var operation = GetOperationById(request.OperationId);
        var role = GetRoleById(request.RoleId);

        if (IsRoleOperationExist(role, operation))
            throw new IncorrectDataException("Role already has this operation");
        
        var roleOperations = new RoleOperation()
        {
            Role = role,
            Operation = operation
        };

        await _repository.Add(roleOperations);
        await _repository.SaveChangesAsync();
    }
    private bool IsRoleOperationExist(Role role, Operation operation)
    {
        var roleOperation =  _repository.Get<RoleOperation>(model => 
            model.Role == role && model.Operation == operation).FirstOrDefault();
        return roleOperation != null;
    }
    private Operation GetOperationById(Guid id)
    {
        var operation =  _repository.Get<Operation>(model => model.Id == id).FirstOrDefault();
        if (operation == null)
            throw new IncorrectDataException("No such operation");
        return operation;
    }
    
    private Role GetRoleById(Guid id)
    {
        var role =  _repository.Get<Role>(model => model.Id == id).FirstOrDefault();
        if (role == null)
            throw new IncorrectDataException("No such role");
        return role;
    }
    
}