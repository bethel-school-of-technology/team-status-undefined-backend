using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace team_status_undefined_backend.Models;

public class Barber
{
    public int BarberId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    [StringLength(2)]
    public string? State { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    [Required]
    [StringLength(7)]
    public string? LicenseNumber { get; set; }

    [Required]
    public string? ProfilePic { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [JsonIgnore]
    public virtual List<BarberImageLink> ImageLinks { get; set; } = new List<BarberImageLink>();
}
