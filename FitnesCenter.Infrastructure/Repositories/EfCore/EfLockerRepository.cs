using Microsoft.EntityFrameworkCore;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Data;

namespace FitnesCenter.Infrastructure.Repositories.EfCore;

public class EfLockerRepository : ILockerRepository
{
    private readonly AppDbContext _context;

    public EfLockerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Locker>> GetAllAsync()
    {
        return await _context.Lockers
            .Include(l => l.Client)
            .ToListAsync();
    }

    public async Task<Locker?> GetByIdAsync(Guid id)
    {
        return await _context.Lockers
            .Include(l => l.Client)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Locker?> GetByNumberAsync(int number)
    {
        return await _context.Lockers
            .FirstOrDefaultAsync(l => l.Number == number);
    }

    public async Task UpdateAsync(Locker locker)
    {
        _context.Lockers.Update(locker);
        await _context.SaveChangesAsync();
    }
}