using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class UserRole : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public UserModel User { get; set; }
    [Required] public Role Role { get; set; }

}