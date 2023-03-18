using team_status_undefined_backend.Models;
using team_status_undefined_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace team_status_undefined_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BarberController : ControllerBase
{
    private readonly ILogger<BarberController> _logger;
    private readonly IBarberRepository _barberRepository;

    public BarberController(ILogger<BarberController> logger, IBarberRepository repository)
    {
        _logger = logger;
        _barberRepository = repository;
    }

    // GET BARBER BY ID METHOD
    [HttpGet]
    [Route("{barberId:int}")]
    public ActionResult<Barber> GetBarberById(int barberId)
    {
        var barber = _barberRepository.GetBarberById(barberId);
        if (barber == null)
        {
            return NotFound();
        }
        return Ok(barber);
    }
    // GET BARBER BY ID METHOD


    // SEARCH METHOD
    [HttpGet]
    [Route("search/{query}")]
    public ActionResult<IEnumerable<Barber>> SearchBarbers(string query)
    {
        return Ok(_barberRepository.SearchBarbers(query));
    }
    // SEARCH METHOD


    // GET ALL BARBERS METHOD
    [HttpGet]
    public ActionResult<IEnumerable<Barber>> GetBarber()
    {
        return Ok(_barberRepository.GetAllBarbers());
    }
    // GET ALL BARBERS METHOD


    // UPDATE BARBER METHOD
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Barber> UpdateBarber(Barber updatedBarber)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        var userId = Int32.Parse(userIdClaim.Value);

        if (!ModelState.IsValid || updatedBarber == null)
        {
            return BadRequest();
        }

        if (userId == updatedBarber.BarberId)
        {
            return Ok(_barberRepository.UpdateBarber(updatedBarber));
        }
        else
        {
            return Unauthorized();
        }
    }
    // UPDATE BARBER METHOD


    // DELETE BARBER METHOD
    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{barberId:int}")]
    public ActionResult DeleteBarber(int barberId)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); //QUESTION THIS
        var userId = Int32.Parse(userIdClaim.Value); 
        var barberToDelete = _barberRepository.GetBarberById(barberId);

        if (userId == null)
        {
            return Unauthorized();
        }

        if (userId == barberToDelete.BarberId)
        {
            _barberRepository.DeleteBarberById(barberId);
            return NoContent();
        }
        else
        {
            return Unauthorized();
        }

        
    }
    // DELETE BARBER METHOD  


    // CREATE USER METHOD
    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(Barber user)
    {
        if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        _barberRepository.CreateUser(user);
        return NoContent();
    }
    // CREATE USER METHOD


    // SIGN IN METHOD
    [HttpPost]
    [Route("login")]
    public ActionResult<string> SignIn(BarberSignIn user)
    {
        if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest();
        }

        var token = _barberRepository.SignIn(user.Email, user.Password);

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }
    // SIGN IN METHOD
}
