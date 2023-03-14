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

    public BarberImageLink? CreateImg(BarberImageLink imageLink)
    {
        _context.Add(imageLink);
        _context.SaveChanges();
        return imageLink;
    }


    public void DeleteBarberImageLinkId(int barberImageLinkId)
    {
        var BarberImageLink = _context.BarberImageLinks.Find(barberImageLinkId);
        if (barberImageLinkId == BarberImageLink.BarberImageLinkId)
        {
            _context.BarberImageLinks.Remove(BarberImageLink);
            _context.SaveChanges();
        }
    }

    public IEnumerable<BarberImageLink> GetAllBarberImgs()
    {
        return _context.BarberImageLinks.ToList();
    }


    // I DON'T THINK WE NEED THIS ONE BUT COMMENTED OUT JUST IN CASE //
    public BarberImageLink? GetImageLinkById(int barberImageLinkId)
    {
        return _context.BarberImageLinks.SingleOrDefault(c => c.BarberImageLinkId == barberImageLinkId);
    }

}
