using book_lending.DTO;
using book_lending.Models.Interfaces;

namespace book_lending.Services.Interfaces;

public interface IAuthorizationService
{
    Task CreateUserAsync(CreateUserRequest request);
    Task<IModels> LoginUser(string login, string password);
}