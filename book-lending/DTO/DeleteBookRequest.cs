namespace book_lending.DTO;

public class DeleteBookRequest
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}