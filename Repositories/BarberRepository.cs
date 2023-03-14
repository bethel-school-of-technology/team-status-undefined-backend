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

    // STATIC???
    public BarberRepository(BarberDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // public Barber CreateBarber(Barber newBarber)
    // {
    //     _context.Barber.Add(newBarber);
    //     _context.SaveChanges();
    //     return newBarber;
    // }

    public void DeleteBarberById(int barberId)
    {
        var barber = _context.Barber.Find(barberId);
        if (barberId == barber.BarberId)
        {
            _context.Barber.Remove(barber);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Barber> SearchBarbers(string search)
    {
        var barber = from c in _context.Barber select c;

        if (!String.IsNullOrEmpty(search))
        {
            barber = barber.Where(c => c.City.Contains(search) || c.FirstName.Contains(search));
        }

        return (barber.ToList());
    }

    public IEnumerable<Barber> GetAllBarbers()
    {
        return _context.Barber.ToList();
    }

    public Barber? GetBarberById(int barberId)
    {
        return _context.Barber.SingleOrDefault(c => c.BarberId == barberId);
    }

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
            // originalBarber.SignInId = newBarber.SignInId;
            //  WE WILL NEED TO EVENTUALLY MAKE THE SIGNINID A KEY VALUE SO IT AUTO INCREMENTS WHEN YOU CREATE A NEW PROFILE
            originalBarber.Email = newBarber.Email;
            originalBarber.Password = newBarber.Password;

            _context.SaveChanges();
        }
        return originalBarber;
    }

    public Barber? CreateUser(Barber user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

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

        // Create token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials
        );

        // Encode token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}
