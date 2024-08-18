using book_lending.DTO;
using book_lending.Exceptions;
using book_lending.Models;
using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;

namespace book_lending.Services;

public class CaretakerService : ICaretakerService
{
    private readonly IDbRepository _repository;
    private readonly IGetModelService _modelService;

    public CaretakerService(IDbRepository repository, IGetModelService modelService)
    {
        _repository = repository;
        _modelService = modelService;
    }

    public async Task AddBook(AddBookRequest request)
    {
        const string requestOperation = "AddBook";


        if (!(await _modelService.IsUserHasPermission(request.UserId, requestOperation)))
            throw new IncorrectDataException($"User does not have permission to operation ({requestOperation})");
        
        var book = new Book()
        {
            Name = request.Name,
            Status = "Factory new"
        };
        await _repository.Add(book);
        await _repository.SaveChangesAsync();
    }

    public async Task<IQueryable<Book>> GetAllBooks()
    {
        return await Task.FromResult(_repository.GetAll<Book>());
    }

   }