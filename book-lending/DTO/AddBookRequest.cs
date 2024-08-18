namespace book_lending.DTO;

public class AddBookRequest
{
    public string Name { get; set; }
    public Guid UserId { get; set; }

    public AddBookRequest(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
    }
}