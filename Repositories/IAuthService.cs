using team_status_undefined_backend.Models;

namespace team_status_undefined_backend.Repositories;

public interface IAuthService
{
    SignIn CreateSignIn(SignIn signin);
    string LogIn (string email, string password);
}