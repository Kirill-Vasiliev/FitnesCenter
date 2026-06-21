using FitnesCenter.Shared.Enums;

namespace FitnesCenter.Domain.Entities;

/// <summary>
/// Тренер фитнес-центра
/// </summary>
public class Trainer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string Phone { get; set; } = string.Empty;
    public TrainerStatus Status { get; set; } = TrainerStatus.WORKING;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}