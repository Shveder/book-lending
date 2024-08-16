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
    public async Task<IActionResult> AddNewRole([FromBody] AddRoleRequest request)
    {
        await _adminService.AddNewRole(request);
        _logger.LogInformation($"Role {request.Name} is created");
        return Ok("Role is added");
    }
}