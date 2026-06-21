using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnesCenter.Infrastructure.Data;

/// <summary>
/// Фабрика для создания DbContext во время разработки (миграции)
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Строка подключения для миграций
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FitnesCenter;Username=postgres;Password=admin123");

        return new AppDbContext(optionsBuilder.Options);
    }
}