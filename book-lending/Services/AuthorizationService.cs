using System.Security.Cryptography;
using System.Text;
using book_lending.DTO;
using book_lending.Exceptions;
using book_lending.Models;
using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace book_lending.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly ILogger<AuthorizationService> _logger;
    private readonly IDbRepository _repository;

    public AuthorizationService(ILogger<AuthorizationService> logger, IDbRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        if (request.Password != request.RepeatPassword)
            throw new IncorrectDataException("Passwords do not match");
        if(await IsLoginUnique(request.Login))
            throw new IncorrectDataException("There is already a user with this login in the system");
        if(request.Login.Length is < 4 or > 32)
            throw new IncorrectDataException("Login length must be between 4 and 32 characters.");
        if(request.Password.Length is < 4 or > 32)
            throw new IncorrectDataException("Password length must be between 4 and 32 characters.");
        
        request.Password = Hash(request.Password);
        string salt = GetSalt();
        request.Password = Hash(request.Password + salt);
        
        var user = new UserModel
        {
            Login = request.Login,
            Password = request.Password,
            Salt = salt,
        };

        await _repository.Add(user);
        await _repository.SaveChangesAsync();
        _logger.LogInformation($"User created (Login: {request.Login})");
    }
    
    private string GetSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }

    private string Hash(string inputString)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
    private async Task<bool> IsLoginUnique(string login)
    {
        var user = await _repository.Get<UserModel>(model => model.Login == login).FirstOrDefaultAsync();
        return user != null;
    }
}