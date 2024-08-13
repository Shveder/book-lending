using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class UserModel : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public string Login { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Salt { get; set; }

}