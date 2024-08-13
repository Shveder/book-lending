using book_lending.DTO;
using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace book_lending.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthorizationController : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger;
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(ILogger<AuthorizationController> logger, IAuthorizationService authorizationService)
    {
        _logger = logger;
        _authorizationService = authorizationService;
    }

    /// <summary>
    ///     Register one user
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Authorization/Register
    /// </remarks>
    /// <returns>
    ///     200 OK with the user data.
    /// </returns>
    /// <response code="200">Returns the list of user data.</response>
    ///  <response code="422">Invalid user data</response>
    ///  <response code="500">Internal server error</response>
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        await _authorizationService.CreateUserAsync(request);
        _logger.LogInformation($"User {request.Login} is created");
        return Ok("Registration success");
    }

}