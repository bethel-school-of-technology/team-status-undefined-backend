using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace team_status_undefined_backend.Models;

public class SignIn {

[JsonIgnore]
public int SignInId { get; set; } 

[Required]
[EmailAddress]
public string? Email { get; set; }

[Required]
[StringLength(8)]
public string? Password { get; set; }

}