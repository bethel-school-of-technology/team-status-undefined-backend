// using l10_rest.Models;
// using l10_rest.Repositories;
// using Microsoft.AspNetCore.Mvc;

// namespace l10_rest.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class CoffeeController : ControllerBase 
// {
//     private readonly ILogger<CoffeeController> _logger;
//     private readonly ICoffeeRepository _coffeeRepository;

//     public CoffeeController(ILogger<CoffeeController> logger, ICoffeeRepository repository)
//     {
//         _logger = logger;
//         _coffeeRepository = repository;
//     }

       [HttpGet]
       public ActionResult<IEnumerable<Barber>> GetBarber() 
        {
        return Ok(_barberRepository.GetAllBarbers());
        }

//     [HttpGet]
//     [Route("{coffeeId:int}")]
//     public ActionResult<Coffee> GetCoffeeById(int coffeeId) 
//     {
//     var coffee = _coffeeRepository.GetCoffeeById(coffeeId);
//     if (coffee == null) {
//         return NotFound();
//     }
//     return Ok(coffee);
//     }

       [HttpPost]
       public ActionResult<Barber> CreateBarber(Barber barber) 
        {
         if (!ModelState.IsValid || barber == null) {
            return BadRequest();
        }
        var newBarber = _barberRepository.CreateBarber(barber);
        return Created(nameof(GetBarberById), newBarber);
        }

//     [HttpPut]
//     [Route("{coffeeId:int}")]
//     public ActionResult<Coffee> UpdateCoffee(Coffee coffee) 
//     {
//     if (!ModelState.IsValid || coffee == null) {
//         return BadRequest();
//     }
//     return Ok(_coffeeRepository.UpdateCoffee(coffee));
//     }

//     [HttpDelete]
//     [Route("{coffeeId:int}")]
//     public ActionResult DeleteCoffee(int coffeeId) 
//     {
//     _coffeeRepository.DeleteCoffeeById(coffeeId); 
//     return NoContent();
//     }
// }