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

//     [HttpGet]
//     public ActionResult<IEnumerable<Coffee>> GetCoffee() 
//     {
//     return Ok(_coffeeRepository.GetAllCoffee());
//     }

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

//     [HttpPost]
// public ActionResult<Coffee> CreateCoffee(Coffee coffee) 
//     {
//     if (!ModelState.IsValid || coffee == null) {
//         return BadRequest();
//     }
//     var newCoffee = _coffeeRepository.CreateCoffee(coffee);
//     return Created(nameof(GetCoffeeById), newCoffee);
//     }

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