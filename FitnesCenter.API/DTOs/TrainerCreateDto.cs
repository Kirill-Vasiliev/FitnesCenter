using System.ComponentModel.DataAnnotations;

namespace FitnesCenter.API.DTOs;

public class TrainerCreateDto
{
    [Required(ErrorMessage = "Фамилия обязательна")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get; set; } = string.Empty;

    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;
}