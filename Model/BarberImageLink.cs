using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace team_status_undefined_backend.Models;

public class BarberImageLink
{
    [Required]
    public string? Title { get; set; }

    [Required]
    public int BarberImageLinkId { get; set; }

    [Required]
    public int BarberId { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    [JsonIgnore]
    public virtual Barber? Barber { get; set; }
}
