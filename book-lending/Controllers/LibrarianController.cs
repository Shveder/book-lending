using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace book_lending.Controllers;

public class LibrarianController : ControllerBase
{
    private readonly ILogger<LibrarianController> _logger;
    private readonly ILibrarianService _librarianService;
    private readonly IGetModelService _modelService;

    public LibrarianController(ILogger<LibrarianController> logger, ILibrarianService librarianService, IGetModelService modelService)
    {
        _logger = logger;
        _librarianService = librarianService;
        _modelService = modelService;
    }
    
    [JsonConverter(typeof(StringEnumConverter<,,>))]
    public enum ItemCondition
    {
        [EnumMember(Value = "Factory new")]
        FactoryNew,

        [EnumMember(Value = "Damaged")]
        Damaged,

        [EnumMember(Value = "Cant be repaired")]
        CantBeRepaired
    }
    
    /// <summary>
    ///     Gives book to user
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Post /Librarian/GiveBook
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200"> Gives book to user.</response>
    /// <response code="422">No access or book damaged.</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("GiveBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GiveBook([FromBody] GiveBookRequest request)
    {
        await _librarianService.GiveBook(request);
        const string answer = "Book is given to user";
        _logger.LogInformation(answer);
        return Ok(answer);
    }
    
    /// <summary>
    ///     Returns book
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Delete /Librarian/ReturnBook
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200"> Returns book.</response>
    /// <response code="422">No access.</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("ReturnBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReturnBook(Guid userId, Guid bookOwnId, [FromQuery] ItemCondition condition)
    {
        await _librarianService.ReturnBook(userId, bookOwnId, condition.ToString());
        const string answer = "Book is returned";
        _logger.LogInformation(answer);
        return Ok(answer);
    }
    
    /// <summary>
    ///     Get all books
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Get /Librarian/GetAllBooks
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
    ///    Get all users
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Get /Librarian/GetAllUsers
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200">Returns list of users.</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _modelService.GetAllUsers());
    }
    
    /// <summary>
    ///    Get all ownerships
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     Get /Librarian/GetAllOwnerships
    /// </remarks>
    /// <returns>
    ///     200 OK.
    /// </returns>
    /// <response code="200">Returns list of ownerships.</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("GetAllOwnerships")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllOwnerships()
    {
        return Ok(await _modelService.GetAllOwnerships());
    }
}