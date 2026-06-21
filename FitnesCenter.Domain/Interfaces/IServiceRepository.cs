using FitnesCenter.Domain.Entities;

namespace FitnesCenter.Domain.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(string id);
}