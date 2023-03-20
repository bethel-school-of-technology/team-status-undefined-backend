using team_status_undefined_backend.Migrations;
using team_status_undefined_backend.Models;
using bcrypt = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace team_status_undefined_backend.Repositories;

public class BarberRepository : IBarberRepository
{
    private readonly BarberDbContext _context;
    private readonly IConfiguration _config;

    public BarberRepository(BarberDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }


    // DeleteBarberById METHOD
    public void DeleteBarberById(int barberId)
    {
        var barber = _context.Barber.Find(barberId);
        if (barber != null)
        {
            _context.Barber.Remove(barber);
            _context.SaveChanges();
        }
    }
    // DeleteBarberById METHOD


    // SearchBarbers METHOD
    public IEnumerable<Barber> SearchBarbers(string search)
    {
        var barber = from c in _context.Barber select c;

        if (!String.IsNullOrEmpty(search))
        {
            barber = barber.Where(c => c.City.Contains(search));
        }

        return (barber.ToList());
    }

    // SearchBarbers METHOD


    // GetAllBarbers METHOD
    public IEnumerable<Barber> GetAllBarbers()
    {
        return _context.Barber.ToList();
    }

    // GetAllBarbers METHOD


    // GetBarberById METHOD
    public Barber? GetBarberById(int barberId)
    {
        return _context.Barber.SingleOrDefault(c => c.BarberId == barberId);
    }

    // GetBarberById METHOD


    // UpdateBarber METHOD
    public Barber? UpdateBarber(Barber newBarber)
    {
        var passwordHash = bcrypt.HashPassword(newBarber.Password);
        newBarber.Password = passwordHash;
        var originalBarber = _context.Barber.Find(newBarber.BarberId);
        if (newBarber.BarberId == originalBarber.BarberId)
        {
            originalBarber.FirstName = newBarber.FirstName;
            originalBarber.LastName = newBarber.LastName;
            originalBarber.Address = newBarber.Address;
            originalBarber.City = newBarber.City;
            originalBarber.State = newBarber.State;
            originalBarber.PhoneNumber = newBarber.PhoneNumber;
            originalBarber.LicenseNumber = newBarber.LicenseNumber;
            originalBarber.ProfilePic = newBarber.ProfilePic;
            originalBarber.Description = newBarber.Description;
            originalBarber.Email = newBarber.Email;
            originalBarber.Password = newBarber.Password;

            _context.SaveChanges();
        }
        return originalBarber;
    }

    // UpdateBarber METHOD


    // CreateUser METHOD
    public Barber? CreateUser(Barber user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    // CreateUser METHOD

    
    // SignIn METHOD
    public string SignIn(string email, string password)
    {
        var user = _context.Barber.SingleOrDefault(x => x.Email == email);
        var verified = false;

        if (user != null)
        {
            verified = bcrypt.Verify(password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }

        return BuildToken(user);
    }

    // SignIn METHOD

    private string BuildToken(Barber user)
    {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.BarberId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}
