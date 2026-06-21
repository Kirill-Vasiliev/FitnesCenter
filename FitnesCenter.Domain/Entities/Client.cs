using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace FitnesCenter.Domain.Entities;

/// <summary>
/// Клиент фитнес-центра
/// </summary>
public class Client
{
    /// <summary>
    /// Уникальный идентификатор (генерируется сервером)
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Фамилия (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Фамилия обязательна")]
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Имя (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Отчество (опционально)
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateOnly Birthday { get; set; }

    /// <summary>
    /// Номер телефона (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Email адрес (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Активен ли клиент (по умолчанию true)
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Ссылка на тренера (опционально, один-ко-многим)
    /// </summary>
    public Guid? TrainerId { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с тренером
    /// </summary>
    public virtual Trainer? Trainer { get; set; }

    // ===== ПОЛЯ ДЛЯ ВТОРОЙ ЧАСТИ =====

    public Guid? LockerId { get; set; }
    public virtual Locker? Locker { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}