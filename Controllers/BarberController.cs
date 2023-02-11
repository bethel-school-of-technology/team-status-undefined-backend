using team_status_undefined_backend.Models;
using team_status_undefined_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

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


//     [HttpGet]
//     public ActionResult<IEnumerable<Coffee>> GetCoffee() 
//     {
//     return Ok(_coffeeRepository.GetAllCoffee());
//     }

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

//     [HttpPost]
// public ActionResult<Coffee> CreateCoffee(Coffee coffee) 
//     {
//     if (!ModelState.IsValid || coffee == null) {
//         return BadRequest();
//     }
//     var newCoffee = _coffeeRepository.CreateCoffee(coffee);
//     return Created(nameof(GetCoffeeById), newCoffee);
//     }

    [HttpPut]
    [Route("{BarberId:int}")]
    public ActionResult<Barber> UpdateBarber(Barber barber) 
    {
    if (!ModelState.IsValid || barber == null) {
        return BadRequest();
    }
    return Ok(_barberRepository.UpdateBarber(barber));
    }

//     [HttpDelete]
//     [Route("{coffeeId:int}")]
//     public ActionResult DeleteCoffee(int coffeeId) 
//     {
//     _coffeeRepository.DeleteCoffeeById(coffeeId); 
//     return NoContent();
//     }
 }
