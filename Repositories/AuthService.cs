using team_status_undefined_backend.Models;
using team_status_undefined_backend.Migrations;
using bcrypt = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace team_status_undefined_backend.Repositories;

public class AuthService : IAuthService
{

    private static BarberDbContext _context;
    private static IConfiguration _config;

    public AuthService(BarberDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    public SignIn CreateSignIn (SignIn signin)
    {
        var passwordHash = bcrypt.HashPassword(signin.Password);
        signin.Password = passwordHash;
        
        _context.Add(signin);
        _context.SaveChanges();
        return signin;
    }

    public string LogIn (string email, string password)
    {
        var user = _context.SignIns.SingleOrDefault(x => x.Email == email);
        var verified = false;

        if (user != null) {
            verified = bcrypt.Verify(password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }

             return BuildToken(user); 
    }

    private string BuildToken(SignIn signin) {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, signin.SignInId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, signin.Email ?? "")
            
        };

    
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials);
        
       
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

}