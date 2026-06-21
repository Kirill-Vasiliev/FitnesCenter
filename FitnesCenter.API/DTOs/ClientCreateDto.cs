using System.ComponentModel.DataAnnotations;

namespace FitnesCenter.API.DTOs;

/// <summary>
/// DTO для создания/обновления клиента
/// </summary>
public class ClientCreateDto
{
    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(100, MinimumLength = 1)]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateOnly Birthday { get; set; }

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Email { get; set; } = string.Empty;
}