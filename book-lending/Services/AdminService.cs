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

    public async Task AddNewRole(AddRoleRequest request)
    {
        if(await IsRoleUnique(request.Name))
            throw new IncorrectDataException("There is already a role with this name in the system");

        var role = new Role()
        {
            RoleName = request.Name
        };

        await _repository.Add(role);
        await _repository.SaveChangesAsync();
        _logger.LogInformation($"New role {request.Name} is added");
    }
    
    private async Task<bool> IsRoleUnique(string name)
    {
        var user = await _repository.Get<Role>(model => model.RoleName == name).FirstOrDefaultAsync();
        return user != null;
    }
}