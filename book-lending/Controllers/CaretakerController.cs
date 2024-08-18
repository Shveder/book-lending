using book_lending.DTO;
using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace book_lending.Controllers;

[ApiController]
[Route("[controller]")]

public class CaretakerController : ControllerBase
{
    private readonly ILogger<CaretakerController> _logger;
    private readonly ICaretakerService _caretakerService;

    public CaretakerController(ILogger<CaretakerController> logger, ICaretakerService caretakerService)
    {
        _logger = logger;
        _caretakerService = caretakerService;
    }
    
    /// <summary>
    ///     Add new book
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Caretaker/AddBook
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200">Returns success.</response>
    ///  <response code="422">Invalid book data</response>
    ///  <response code="500">Internal server error</response>
    [HttpPost("AddBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddBook([FromBody] AddBookRequest request)
    {
        await _caretakerService.AddBook(request);
        var answer = $"Book {request.Name} is added";
        _logger.LogInformation(answer);
        return Ok(answer);
    }
    
    /// <summary>
    ///     Add new book
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Get /Caretaker/GetAllBooks
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
        return Ok(await _caretakerService.GetAllBooks());
    }
    
    /// <summary>
    ///     Delete damaged books that can't be repaired
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Delete /Caretaker/DeleteBook
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200">Deletes damaged book.</response>
    /// <response code="422">No access or no book.</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("DeleteBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBook([FromBody] DeleteBookRequest request)
    {
        await _caretakerService.DeleteBook(request);
        return Ok();
    }
    
    
}