using book_lending.DTO;

namespace book_lending.Services.Interfaces;

public interface IAuthorizationService
{
    Task CreateUserAsync(CreateUserRequest request);
}