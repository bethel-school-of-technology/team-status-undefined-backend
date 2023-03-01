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



    [HttpGet]
    [Route("{barberId:int}")]
    public ActionResult<Barber> GetBarberById(int barberId) 
    {
    var barber = _barberRepository.GetBarberById(barberId);
    if (barber == null) {
        return NotFound();
    }
    return Ok(barber);
    }

     [HttpGet]
    [Route("search/{query}")]
    public ActionResult<IEnumerable<Barber>> SearchBarbers(string query) 
    {
   
    return Ok(_barberRepository.SearchBarbers(query));

    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Barber>> GetBarber() 
    {
    return Ok(_barberRepository.GetAllBarbers());
    }




    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{BarberId:int}")]
    public ActionResult<Barber> UpdateBarber(Barber barber) 
    {
    if (!ModelState.IsValid || barber == null) {
        return BadRequest();
    }
    return Ok(_barberRepository.UpdateBarber(barber));
    }





    // [HttpPost]
    // public ActionResult<Barber> CreateBarber(Barber barber) 
    // {
    //     if (!ModelState.IsValid || barber == null) {
    //     return BadRequest();
    // }
    //     var newBarber = _barberRepository.CreateBarber(barber);
    //     return Created(nameof(GetBarberById), newBarber);
    // }



    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{barberId:int}")]
    public ActionResult DeleteBarber(int barberId) 
    {
        _barberRepository.DeleteBarberById(barberId); 
        return NoContent();
    }


    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(Barber user) 
    {
    if (user == null || !ModelState.IsValid) {
        return BadRequest();
    }
    _barberRepository.CreateUser(user);
        return NoContent();
    }


    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string email, string password) 
    {
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
    {
        return BadRequest();
    }

    var token = _barberRepository.SignIn(email, password);

    if (string.IsNullOrWhiteSpace(token)) {
        return Unauthorized();
    }

        return Ok(token);
    }
}