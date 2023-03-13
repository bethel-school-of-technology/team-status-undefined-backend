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

    [HttpGet]
    public ActionResult<BarberImageLink> GetImgs()
    {
        return Ok(_barberImgRepository.GetAllBarberImgs());
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{barberImageLinkId:int}")]
    public ActionResult DeleteImage(int barberImageLinkId)
    {
        _barberImgRepository.DeleteBarberImageLinkId(barberImageLinkId);
        return NoContent();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<BarberImageLink> CreateImage(BarberImageLink image)
    {
        if (!ModelState.IsValid || image == null) {
        return BadRequest();
    }
        var newImage = _barberImgRepository.CreateImg(image);
        return Created(nameof(GetImageLinkById), newImage);
    }

    [HttpGet]
    [Route("{barberImageLinkId:int}")]
    public ActionResult<BarberImageLink> GetImageLinkById(int barberImageLinkId)
    {
        var img = _barberImgRepository.GetImageLinkById(barberImageLinkId);
        if (img == null)
        {
            return NotFound();
        }
        return Ok(img);
    }

}