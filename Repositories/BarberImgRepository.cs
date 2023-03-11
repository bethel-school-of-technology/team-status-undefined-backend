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
        var BarberImageLink = _context.Barber.Find(barberImageLink);
        if (barberImageLink != null)
        {
            _context.Barber.Remove(BarberImageLink);
            _context.SaveChanges();
        }
    }

    public IEnumerable<BarberImageLink> GetAllBarberImgs()
    {
        return _context.Barber.ToList();
    }


    // I DON'T THINK WE NEED THIS ONE BUT COMMENTED OUT JUST IN CASE //
    // public BarberImageLink? GetBarberImageLinkId(int barberImageLinkId)
    // {
    //     return _context.Barber.SingleOrDefault(c => c.BarberImageLinkId == barberImageLinkId);
    // }

}
