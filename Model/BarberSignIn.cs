using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace team_status_undefined_backend.Models;

public class BarberSignIn
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
