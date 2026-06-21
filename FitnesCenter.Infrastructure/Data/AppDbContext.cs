using Microsoft.EntityFrameworkCore;
using FitnesCenter.Domain.Entities;

namespace FitnesCenter.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Locker> Lockers { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Client - Trainer
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Trainer)
            .WithMany(t => t.Clients)
            .HasForeignKey(c => c.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Client - Locker (один к одному)
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Locker)
            .WithOne(l => l.Client)
            .HasForeignKey<Client>(c => c.LockerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Уникальный номер шкафчика
        modelBuilder.Entity<Locker>()
            .HasIndex(l => l.Number)
            .IsUnique();

        // Client - Service (многие ко многим)
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Services)
            .WithMany(s => s.Clients)
            .UsingEntity(j => j.ToTable("ClientServices"));

        // Seed данные
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // 5 услуг
        modelBuilder.Entity<Service>().HasData(
            new Service { Id = "SOLARIUM", Name = "Солярий", Price = 400 },
            new Service { Id = "POOL", Name = "Бассейн", Price = 200 },
            new Service { Id = "SAUNA", Name = "Сауна", Price = 0 },
            new Service { Id = "CRYOSAUNA", Name = "Криосауна", Price = 1000 },
            new Service { Id = "CROSSFIT", Name = "Кроссфит", Price = 500 }
        );

        // 20 шкафчиков
        for (int i = 1; i <= 20; i++)
        {
            modelBuilder.Entity<Locker>().HasData(
                new Locker { Id = Guid.NewGuid(), Number = i }
            );
        }

        // Тренеры
        modelBuilder.Entity<Trainer>().HasData(
            new Trainer
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Surname = "Иванов",
                Name = "Алексей",
                Patronymic = "Сергеевич",
                Phone = "+7-999-111-11-11",
                Status = Shared.Enums.TrainerStatus.WORKING
            },
            new Trainer
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Surname = "Петрова",
                Name = "Мария",
                Patronymic = "Ивановна",
                Phone = "+7-999-444-44-44",
                Status = Shared.Enums.TrainerStatus.WORKING
            }
        );

        // Клиенты
        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Surname = "Петров",
                Name = "Иван",
                Patronymic = "Алексеевич",
                Birthday = new DateOnly(1990, 5, 15),
                Phone = "+7-999-222-22-22",
                Email = "ivan@example.com",
                IsActive = true,
                TrainerId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            },
            new Client
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Surname = "Сидоров",
                Name = "Петр",
                Patronymic = "Иванович",
                Birthday = new DateOnly(1985, 8, 20),
                Phone = "+7-999-333-33-33",
                Email = "petr@example.com",
                IsActive = true,
                TrainerId = null
            }
        );
    }
}