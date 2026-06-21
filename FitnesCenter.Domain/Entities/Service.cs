namespace FitnesCenter.Domain.Entities;

/// <summary>
/// Дополнительная услуга
/// </summary>
public class Service
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}