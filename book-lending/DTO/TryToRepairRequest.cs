namespace book_lending.DTO;

public class TryToRepairRequest
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}