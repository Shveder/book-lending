namespace book_lending.DTO;

public class AddOperationToRoleRequest
{
    public Guid RoleId { get; set; }
    public Guid OperationId { get; set; }
}