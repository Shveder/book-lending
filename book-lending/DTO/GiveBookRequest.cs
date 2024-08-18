namespace book_lending.DTO;

public class GiveBookRequest
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

    public GiveBookRequest(Guid bookId, Guid userId)
    {
        BookId = bookId;
        UserId = userId;
    }
}