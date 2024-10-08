﻿using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;

namespace book_lending.Services;

public class GetModelService : IGetModelService
{
    private readonly IDbRepository _repository;

    public GetModelService(IDbRepository repository)
    {
        _repository = repository;
    }

    public Operation GetOperationById(Guid id)
    {
        var operation =  _repository.Get<Operation>(model => model.Id == id).FirstOrDefault();
        if (operation == null)
            throw new IncorrectDataException("No such operation");
        return operation;
    }
    
    public Role GetRoleById(Guid id)
    {
        var role =  _repository.Get<Role>(model => model.Id == id).FirstOrDefault();
        if (role == null)
            throw new IncorrectDataException("No such role");
        return role;
    }
    public UserModel GetUserById(Guid id)
    {
        var user =  _repository.Get<UserModel>(model => model.Id == id).FirstOrDefault();
        if (user == null)
            throw new IncorrectDataException("No such user");
        return user;
    }
    public Book GetBookById(Guid bookId)
    {
        var book =  _repository.Get<Book>(book => book.Id == bookId).FirstOrDefault();
        if (book == null)
            throw new IncorrectDataException("No such book");
        return book;
    }
    public BookOwnership GetBookOwnership(Guid bookOwnId)
    {
        var bookOwn =  _repository.Get<BookOwnership>(bookOwn =>
            bookOwn.Id == bookOwnId).Include(model=>model.Book).FirstOrDefault();
        if (bookOwn == null)
            throw new IncorrectDataException("No such book ownership");
        return bookOwn;
    }public bool IsBookAvailable(Guid bookId)
    { 
        var bookOwn =  _repository.Get<BookOwnership>(bookOwn => bookOwn.Book.Id == bookId).FirstOrDefault();
        return bookOwn == null;
    }
    public async Task<List<UserRole>> GetUserRoles(Guid userId)
    {
        var user = GetUserById(userId);
        return await _repository.GetAll<UserRole>().Where(role => role.User == user)
            .Include(model=> model.Role).ToListAsync();
    }
    public async Task<List<BookOwnership>> GetUserBooks(Guid userId)
    {
        var user = GetUserById(userId);
        return await _repository.GetAll<BookOwnership>().Where(bookOwn => bookOwn.User == user)
            .Include(bookOwn=> bookOwn.Book).ToListAsync();
    }
    public async Task<List<RoleOperation>> GetRoleOperations(Guid roleId)
    {
        var role = GetRoleById(roleId);
        return await _repository.GetAll<RoleOperation>().Where(model => model.Role == role)
            .Include(model=> model.Operation).ToListAsync();
    }
    public async Task<bool> IsUserHasPermission(Guid userId, string requestOperation)
    {
        var userRoles = await GetUserRoles(userId);
        foreach (var userRole in userRoles)
        {
            var operations = await GetRoleOperations(userRole.Role.Id);

            if (operations.Select(operation => GetOperationById(operation.Operation.Id))
                .Any(accessOperation => accessOperation.OperationName.Contains(requestOperation)))
            {
                return true;
            }
        }
        return false;
    }
    public async Task<IQueryable<Book>> GetAllBooks()
    {
        return await Task.FromResult(_repository.GetAll<Book>());
    }
    public async Task<IQueryable<UserModel>> GetAllUsers()
    {
        return await Task.FromResult(_repository.GetAll<UserModel>());
    }

    public async Task<IQueryable<BookOwnership>> GetAllOwnerships()
    {
        return await Task.FromResult(_repository.GetAll<BookOwnership>());
    }
}