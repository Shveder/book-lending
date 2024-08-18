using book_lending.Repository.Interface;
using book_lending.Services.Interfaces;

namespace book_lending.Services;

public class HandymanService : IHandymanService
{
    private readonly IDbRepository _repository;
    private readonly IGetModelService _modelService;
    private static readonly Random Random = new Random();


    public HandymanService(IDbRepository repository, IGetModelService modelService)
    {
        _repository = repository;
        _modelService = modelService;
    }

    public async Task<string> TryToRepairBook(TryToRepairRequest request)
    {
        const string requestOperation = "RepairBook";
        
        if (!(await _modelService.IsUserHasPermission(request.UserId, requestOperation)))
            throw new IncorrectDataException($"User does not have permission to operation ({requestOperation})");
        
        var book = _modelService.GetBookById(request.BookId);
        if(book.Status == "Factory new")
            throw new IncorrectDataException("This book is not damaged");
        if(book.Status == "Cant be repaired")
            throw new IncorrectDataException("This book cant be repaired");
        
        var chance = Random.Next(0, 100);
        book.Status = chance < 25 ? "Cant be repaired" : "Factory new";
        book.DateUpdated = DateTime.UtcNow;

        await _repository.Update(book);
        await _repository.SaveChangesAsync();
        return $"New status of book is {book.Status}";
    }
}