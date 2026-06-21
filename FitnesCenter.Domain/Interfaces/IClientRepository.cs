using FitnesCenter.Domain.Entities;

namespace FitnesCenter.Domain.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client?> GetDetailAsync(Guid id);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Guid id);
}