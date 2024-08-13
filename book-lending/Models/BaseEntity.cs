using book_lending.Models.Interfaces;

namespace book_lending.Models;

public class BaseEntity : IModels
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }
    public Guid Id { get; set; }
}