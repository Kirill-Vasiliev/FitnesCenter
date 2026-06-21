using FitnesCenter.Domain.Entities;

namespace FitnesCenter.Domain.Interfaces;

public interface ITrainerRepository
{
    Task<IEnumerable<Trainer>> GetAllAsync();
    Task<Trainer?> GetByIdAsync(Guid id);
    Task<Trainer?> GetDetailAsync(Guid id);
    Task AddAsync(Trainer trainer);
    Task UpdateAsync(Trainer trainer);
}