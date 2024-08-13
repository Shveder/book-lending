using System.ComponentModel.DataAnnotations;

namespace book_lending.Models;

public class Operation : BaseEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public string OperationName { get; set; }
    
}