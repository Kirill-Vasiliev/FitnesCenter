using Microsoft.EntityFrameworkCore;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Data;

namespace FitnesCenter.Infrastructure.Repositories.EfCore;

public class EfClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public EfClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client?> GetDetailAsync(Guid id)
    {
        return await _context.Clients
            .Include(c => c.Trainer)
            .Include(c => c.Locker)
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}