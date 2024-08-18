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
        var chance = Random.Next(0, 100);
        var book = _modelService.GetBookById(request.BookId);
        
        book.Status = chance < 25 ? "Cant be repaired" : "Factory new";
        book.DateUpdated = DateTime.UtcNow;

        await _repository.Update(book);
        await _repository.SaveChangesAsync();
        return $"New status of book is {book.Status}";
    }
}