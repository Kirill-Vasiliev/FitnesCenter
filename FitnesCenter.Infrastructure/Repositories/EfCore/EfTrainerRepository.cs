using Microsoft.EntityFrameworkCore;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Data;

namespace FitnesCenter.Infrastructure.Repositories.EfCore;

public class EfTrainerRepository : ITrainerRepository
{
    private readonly AppDbContext _context;

    public EfTrainerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trainer>> GetAllAsync()
    {
        return await _context.Trainers.ToListAsync();
    }

    public async Task<Trainer?> GetByIdAsync(Guid id)
    {
        return await _context.Trainers.FindAsync(id);
    }

    public async Task<Trainer?> GetDetailAsync(Guid id)
    {
        return await _context.Trainers
            .Include(t => t.Clients)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Trainer trainer)
    {
        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Trainer trainer)
    {
        _context.Trainers.Update(trainer);
        await _context.SaveChangesAsync();
    }
}