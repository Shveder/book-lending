using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class Book : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Status { get; set; }
    
}