using FitnesCenter.Domain.Entities;

namespace FitnesCenter.Domain.Interfaces;

public interface ILockerRepository
{
    Task<IEnumerable<Locker>> GetAllAsync();
    Task<Locker?> GetByIdAsync(Guid id);
    Task<Locker?> GetByNumberAsync(int number);
    Task UpdateAsync(Locker locker);
}