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
    public ActionResult<IEnumerable<Barber>> GetBarber() 
    {
    return Ok(_barberRepository.GetAllBarbers());
    }




    [HttpPut]
    [Route("{BarberId:int}")]
    public ActionResult<Barber> UpdateBarber(Barber barber) 
    {
    if (!ModelState.IsValid || barber == null) {
        return BadRequest();
    }
    return Ok(_barberRepository.UpdateBarber(barber));
    }





    [HttpPost]
    public ActionResult<Barber> CreateBarber(Barber barber) 
    {
        if (!ModelState.IsValid || barber == null) {
        return BadRequest();
    }
    var newBarber = _barberRepository.CreateBarber(barber);
    return Created(nameof(GetBarberById), newBarber);
    }



    [HttpDelete]
    [Route("{barberId:int}")]
    public ActionResult DeleteBarber(int barberId) 
    {
    _barberRepository.DeleteBarberById(barberId); 
    return NoContent();
    }
}