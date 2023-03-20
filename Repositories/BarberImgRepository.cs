using team_status_undefined_backend.Migrations;
using team_status_undefined_backend.Models;

namespace team_status_undefined_backend.Repositories;

public class BarberImgRepository : IBarberImgRepository
{
    private readonly BarberDbContext _context;
    private readonly IConfiguration _config;

    public BarberImgRepository(BarberDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // Create Imgage METHOD
    public BarberImageLink? CreateImg(BarberImageLink imageLink)
    {
        _context.Add(imageLink);
        _context.SaveChanges();
        return imageLink;
    }

    // Create Imgage METHOD


    // Delete Image by ImageLinkId METHOD
    public void DeleteBarberImageLinkId(int barberImageLinkId)
    {
        var BarberImageLink = _context.BarberImageLinks.Find(barberImageLinkId);
        if (barberImageLinkId == BarberImageLink.BarberImageLinkId)
        {
            _context.BarberImageLinks.Remove(BarberImageLink);
            _context.SaveChanges();
        }
    }

    // Delete Image by ImageLinkId METHOD


    // Get All Barber Images METHOD
    public IEnumerable<BarberImageLink> GetAllBarberImgs()
    {
        return _context.BarberImageLinks.ToList(); 
    }

    // Get All Barber Images METHOD

    
    // Get Image By BarberId METHOD
    public IEnumerable<BarberImageLink> GetImageByBarberId(int barberId)
    {
        return _context.BarberImageLinks.Where(
            c => c.BarberId == barberId
        );
    }
    // Get Image By BarberId METHOD

    //Get Image By ImageLinkId

    public BarberImageLink? GetImageByImageLinkId(int barberImageLinkId)
    {
        return _context.BarberImageLinks.SingleOrDefault(
            c => c.BarberImageLinkId == barberImageLinkId
        );
    }

    //Get Image By ImageLinkId
}
