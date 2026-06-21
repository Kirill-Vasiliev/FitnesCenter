using Microsoft.EntityFrameworkCore;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Data;

namespace FitnesCenter.Infrastructure.Repositories.EfCore;

public class EfServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public EfServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(string id)
    {
        return await _context.Services
            .Include(s => s.Clients)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}