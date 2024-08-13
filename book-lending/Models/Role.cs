using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class Role : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public string RoleName { get; set; }
}