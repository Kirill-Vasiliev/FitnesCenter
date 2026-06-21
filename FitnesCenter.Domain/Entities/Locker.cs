namespace FitnesCenter.Domain.Entities;

/// <summary>
/// Шкафчик в фитнес-центре
/// </summary>
public class Locker
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public Guid? ClientId { get; set; }
    public virtual Client? Client { get; set; }
}