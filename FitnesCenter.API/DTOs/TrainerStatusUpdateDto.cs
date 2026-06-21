using System.ComponentModel.DataAnnotations;

namespace FitnesCenter.API.DTOs;

public class TrainerStatusUpdateDto
{
    [Required(ErrorMessage = "Статус обязателен")]
    [RegularExpression("WORKING|ON_LEAVE|NOT_WORKING",
        ErrorMessage = "Статус должен быть: WORKING, ON_LEAVE или NOT_WORKING")]
    public string Status { get; set; } = string.Empty;
}