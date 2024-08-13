using book_lending.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace book_lending.Controllers;

public class LibrarianController : ControllerBase
{
    private readonly ILogger<LibrarianController> _logger;
    private readonly ILibrarianService _librarianService;

    public LibrarianController(ILogger<LibrarianController> logger, ILibrarianService librarianService)
    {
        _logger = logger;
        _librarianService = librarianService;
    }
    
    
}