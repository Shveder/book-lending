using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class BookOwnership : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public DateTime ReturnDate { get; set; }
    [Required] public UserModel User { get; set; }
    [Required] public Book Book { get; set; }
}