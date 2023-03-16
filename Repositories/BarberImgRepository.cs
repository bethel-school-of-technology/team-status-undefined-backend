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

    // CreateImg METHOD
    public BarberImageLink? CreateImg(BarberImageLink imageLink)
    {
        _context.Add(imageLink);
        _context.SaveChanges();
        return imageLink;
    }

    // CreateImg METHOD


    // DeleteBarberImageLinkId METHOD
    public void DeleteBarberImageLinkId(int barberImageLinkId)
    {
        var BarberImageLink = _context.BarberImageLinks.Find(barberImageLinkId);
        if (barberImageLinkId == BarberImageLink.BarberImageLinkId)
        {
            _context.BarberImageLinks.Remove(BarberImageLink);
            _context.SaveChanges();
        }
    }

    // DeleteBarberImageLinkId METHOD


    // GetAllBarberImgs METHOD
    public IEnumerable<BarberImageLink> GetAllBarberImgs()
    {
        return _context.BarberImageLinks.ToList();
    }

    // GetAllBarberImgs METHOD

    
    // GetImageLinkById METHOD
    public BarberImageLink? GetImageLinkById(int barberImageLinkId)
    {
        return _context.BarberImageLinks.SingleOrDefault(
            c => c.BarberImageLinkId == barberImageLinkId
        );
    }
    // GetImageLinkById METHOD
}
