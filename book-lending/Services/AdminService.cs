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
    private readonly IGetModelService _modelService;

    public AdminService(IDbRepository repository, ILogger<AuthorizationService> logger, IGetModelService modelService)
    {
        _repository = repository;
        _logger = logger;
        _modelService = modelService;
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

    public async Task<IQueryable<UserModel>> GetUsers()
    {
        return await Task.FromResult(_repository.GetAll<UserModel>());
    }
    private async Task<bool> IsOperationUnique(string name)
    {
        var operation = await _repository.Get<Operation>(model => model.OperationName == name).FirstOrDefaultAsync();
        return operation != null;
    }

    public async Task AddOperationToRole(AddOperationToRoleRequest request)
    {
        var operation = _modelService.GetOperationById(request.OperationId);
        var role = _modelService.GetRoleById(request.RoleId);

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
    public async Task AddRoleToUser(AddRoleToUserRequest request)
    {
       var role = _modelService.GetRoleById(request.RoleId);
       var user = _modelService.GetUserById(request.UserId);

        if (IsUserRoleExist(role, user))
            throw new IncorrectDataException("User already has this role");
        
        var userRole = new UserRole()
        {
            Role = role,
            User = user
        };

        await _repository.Add(userRole);
        await _repository.SaveChangesAsync();
    }
    private bool IsUserRoleExist(Role role, UserModel user)
    {
        var userRole =  _repository.Get<UserRole>(model => 
            model.Role == role && model.User == user).FirstOrDefault();
        return userRole != null;
    }

}