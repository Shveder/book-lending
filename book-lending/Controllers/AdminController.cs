using book_lending.DTO;
using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace book_lending.Controllers;


[ApiController]
[Route("[controller]")]

public class AdminController : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger;
    private readonly IAdminService _adminService;

    public AdminController(ILogger<AuthorizationController> logger, IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }
    
    /// <summary>
    ///     Add new role
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/AddNewRole
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Role is added</response>
    /// <response code="422">Invalid role data</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("AddNewRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddNewRole([FromBody] string role)
    {
        await _adminService.AddNewRole(role);
        _logger.LogInformation($"Role {role} is created");
        return Ok("Role is added");
    }
    
    /// <summary>
    ///     Add new operation
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/AddNewOperation
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Operation is added</response>
    /// <response code="422">Invalid operation data</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("AddNewOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddNewOperation([FromBody] string operation)
    {
        await _adminService.AddNewOperation(operation);
        _logger.LogInformation($"Operation {operation} is created");
        return Ok("Operation is added");
    }
    
    /// <summary>
    ///     Get all roles
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/GetRoles
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Roles are got </response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetRoles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoles()
    {
        _logger.LogInformation("Roles is got");
        return Ok(await _adminService.GetRoles());
    }
    
    /// <summary>
    ///     Get all operations
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/GetOperations
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Operations are got </response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetOperations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOperations()
    {
        _logger.LogInformation("Operations is got");
        return Ok(await _adminService.GetOperations());
    }
    
    /// <summary>
    ///     Get all users
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/GetUsers
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Users are got </response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("GetUsers is got");
        return Ok(await _adminService.GetUsers());
    }
    
    /// <summary>
    ///     Add operation to role
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/AddOperationToRole
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Operation to role is added</response>
    /// <response code="422">Invalid data</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("AddOperationToRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddOperationToRole([FromBody] AddOperationToRoleRequest request)
    {
        await _adminService.AddOperationToRole(request);
        _logger.LogInformation("Operation to role is added");
        return Ok("Operation to role is added");
    }
    
    /// <summary>
    ///     Add role to user
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Admin/AddRoleToUser
    /// </remarks>
    /// <returns>
    ///     200 OK 
    /// </returns>
    /// <response code="200">Role is added to user</response>
    /// <response code="422">Invalid data</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("AddRoleToUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleToUserRequest request)
    {
        await _adminService.AddRoleToUser(request);
        const string answer = "Role is added to user";
        _logger.LogInformation(answer);
        return Ok(answer);
    }
}