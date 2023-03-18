using team_status_undefined_backend.Models;
using team_status_undefined_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace team_status_undefined_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;
    private readonly IBarberImgRepository _barberImgRepository;

    public ImageController(ILogger<ImageController> logger, IBarberImgRepository repository)
    {
        _logger = logger;
        _barberImgRepository = repository;
    }

    // GET ALL IMAGES METHOD
    [HttpGet]
    public ActionResult<BarberImageLink> GetImgs()
    {
        return Ok(_barberImgRepository.GetAllBarberImgs());
    }

    // GET ALL IMAGES METHOD


    // DELETE IMAGE METHOD
    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{barberImageLinkId:int}")]
    public ActionResult DeleteImage(int barberImageLinkId)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); //QUESTION THIS
        var userId = Int32.Parse(userIdClaim.Value); 
        var imageToDelete = _barberImgRepository.GetImageByImageLinkId(barberImageLinkId);

        if (userId == null)
        {
            return Unauthorized();
        }

        if (userId == imageToDelete.BarberId)
        {
            _barberImgRepository.DeleteBarberImageLinkId(barberImageLinkId);
            return NoContent();
        }
        else
        {
            return Unauthorized();
        }
    }

    // DELETE IMAGE METHOD


    // CREATE IMAGE METHOD
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<BarberImageLink> CreateImage(BarberImageLink imageLink)
    {
        if (HttpContext.User == null)
        {
            return Unauthorized();
        }

        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); //QUESTION THIS
        var userId = Int32.Parse(userIdClaim.Value); 
        

        if (userId == imageLink.BarberId)
        {
            return Ok(_barberImgRepository.CreateImg(imageLink));
        }
        else
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid || imageLink == null)
        {
            return BadRequest();
        }

    }

    // CREATE IMAGE METHOD

    
    // GET IMAGE BY BARBERID METHOD
    [HttpGet]
    [Route("{barberId:int}")]
    public ActionResult GetImageByBarberId(int barberId)
    {
        var img = _barberImgRepository.GetImageByBarberId(barberId);
        if (img == null)
        {
            return NotFound();
        }
        return Ok(img);
    }
    // GET IMAGE BY ID METHOD
}
