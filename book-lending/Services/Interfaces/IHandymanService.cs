namespace book_lending.Services.Interfaces;

public interface IHandymanService
{
    Task<string> TryToRepairBook(TryToRepairRequest request);
}