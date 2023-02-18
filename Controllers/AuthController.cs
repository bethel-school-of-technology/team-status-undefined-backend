using team_status_undefined_backend.Repositories;
using team_status_undefined_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace team_status_undefined_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        _logger = logger;
        _authService = service;
    }

     [HttpPost]
    [Route("register")]
    public ActionResult CreateSignIn (SignIn signin)  
    {
        if (signin == null || !ModelState.IsValid) {
            return BadRequest();
        }
        _authService.CreateSignIn(signin);
        return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> LogIn(string email, string password) 
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        var token = _authService.LogIn(email, password);

        if (string.IsNullOrWhiteSpace(token)) {
            return Unauthorized();
        }

        return Ok(token);
    }
}