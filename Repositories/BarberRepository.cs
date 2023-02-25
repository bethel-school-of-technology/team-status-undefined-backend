using team_status_undefined_backend.Migrations;
using team_status_undefined_backend.Models;

namespace team_status_undefined_backend.Repositories;

public class BarberRepository : IBarberRepository 
{
    private readonly BarberDbContext _context;

    public BarberRepository(BarberDbContext context)
    {
        _context = context;
    }

    public Barber CreateBarber(Barber newBarber)
    {
        _context.Barber.Add(newBarber);
        _context.SaveChanges();
        return newBarber;
    }

    public void DeleteBarberById(int barberId)
    {
        var barber = _context.Barber.Find(barberId);
        if (barber != null) {
            _context.Barber.Remove(barber); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Barber> GetAllBarbers()
    {
        return _context.Barber.ToList();
    }

    public IEnumerable<Barber> SearchBarbers(string search)
    {
        if(_context.Barber == null)
        {
            return _context.Barber.FirstOrDefault(c => c.FirstName == (barber.ToList()));
        }

        var barber = from c in _context.Barber select c;

        if (!String.IsNullOrEmpty(search))
        {
            barber = barber.Where( c => c.City.Contains(search) ||
                                      c.FirstName.Contains (search));
        }

        return (barber.ToList());

        }

    public Barber? GetBarberById(int barberId)
    {
        return _context.Barber.SingleOrDefault(c => c.BarberId == barberId);
    }


    public Barber? UpdateBarber(Barber newBarber)
    {
        var originalBarber = _context.Barber.Find(newBarber.BarberId);
        if (originalBarber != null) {
            originalBarber.FirstName = newBarber.FirstName;
            originalBarber.LastName = newBarber.LastName;
            originalBarber.Address = newBarber.Address;
            originalBarber.City = newBarber.City;
            originalBarber.State = newBarber.State;
            originalBarber.PhoneNumber = newBarber.PhoneNumber;
            originalBarber.LicenseNumber = newBarber.LicenseNumber;
            originalBarber.ProfilePic = newBarber.ProfilePic;
            originalBarber.Description = newBarber.Description;
            _context.SaveChanges();
        }
        return originalBarber;
    }

   
}
