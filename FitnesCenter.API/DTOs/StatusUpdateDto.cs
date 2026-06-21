using System.ComponentModel.DataAnnotations;

namespace FitnesCenter.API.DTOs;

public class StatusUpdateDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}