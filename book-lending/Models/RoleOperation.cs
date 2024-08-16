using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class RoleOperation : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public Operation Operation { get; set; }
    [Required] public Role Role { get; set; }
}