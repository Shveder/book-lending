using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace book_lending.Controllers;

[ApiController]
[Route("[controller]")]
public class HandymanController : ControllerBase
{
    private readonly ILogger<HandymanController> _logger;
    private readonly IHandymanService _handymanService;
    private readonly IGetModelService _modelService;

    public HandymanController(ILogger<HandymanController> logger, IHandymanService handymanService, IGetModelService modelService)
    {
        _logger = logger;
        _handymanService = handymanService;
        _modelService = modelService;
    }
    
    /// <summary>
    ///     Get all books
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Get /Handyman/GetAllBooks
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200">Returns list of books.</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetAllBooks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok(await _modelService.GetAllBooks());
    }
    
    /// <summary>
    ///     Tries to repair book 
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Put /Handyman/TryToRepairBook
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200"> Trying to repair book.</response>
    /// <response code="422">No access.</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("TryToRepairBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TryToRepairBook([FromBody] TryToRepairRequest request)
    {
        string answer = await _handymanService.TryToRepairBook(request);
        _logger.LogInformation(answer);
        return Ok(answer);
    }
    
}