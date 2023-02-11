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


       [HttpGet]
       public ActionResult<IEnumerable<Barber>> GetBarber() 
        {
        return Ok(_barberRepository.GetAllBarbers());
        }


    // [HttpGet]
    // [Route("{coffeeId:int}")]
    // public ActionResult<Coffee> GetCoffeeById(int coffeeId) 
    // {
    // var coffee = _coffeeRepository.GetCoffeeById(coffeeId);
    // if (coffee == null) {
    //     return NotFound();
    // }
    // return Ok(coffee);
    // }

       [HttpPost]
       public ActionResult<Barber> CreateBarber(Barber barber) 
        {
         if (!ModelState.IsValid || barber == null) {
            return BadRequest();
        }
        var newBarber = _barberRepository.CreateBarber(barber);
        return Created(nameof(GetBarberById), newBarber);
        }

    // [HttpPut]
    // [Route("{coffeeId:int}")]
    // public ActionResult<Coffee> UpdateCoffee(Coffee coffee) 
    // {
    // if (!ModelState.IsValid || coffee == null) {
    //     return BadRequest();
    // }
    // return Ok(_coffeeRepository.UpdateCoffee(coffee));
    // }

    [HttpDelete]
    [Route("{barberId:int}")]
    public ActionResult DeleteBarber(int barberId) 
    {
    _barberRepository.DeleteBarberById(barberId); 
    return NoContent();
    }
}