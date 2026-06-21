using System.Collections.Concurrent;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;

namespace FitnesCenter.Infrastructure.Repositories.InMemory;

public class InMemoryTrainerRepository : ITrainerRepository
{
    private readonly ConcurrentDictionary<Guid, Trainer> _trainers = new();
    private readonly ConcurrentDictionary<Guid, Client> _clients = new();

    public InMemoryTrainerRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var trainer1 = new Trainer
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Surname = "Иванов",
            Name = "Алексей",
            Patronymic = "Сергеевич",
            Phone = "+7-999-111-11-11",
            Status = Shared.Enums.TrainerStatus.WORKING
        };
        _trainers.TryAdd(trainer1.Id, trainer1);

        var trainer2 = new Trainer
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Surname = "Петрова",
            Name = "Мария",
            Patronymic = "Ивановна",
            Phone = "+7-999-444-44-44",
            Status = Shared.Enums.TrainerStatus.WORKING
        };
        _trainers.TryAdd(trainer2.Id, trainer2);
    }

    public Task<IEnumerable<Trainer>> GetAllAsync()
        => Task.FromResult(_trainers.Values.AsEnumerable());

    public Task<Trainer?> GetByIdAsync(Guid id)
    {
        _trainers.TryGetValue(id, out var trainer);
        return Task.FromResult(trainer);
    }

    public Task<Trainer?> GetDetailAsync(Guid id)
    {
        _trainers.TryGetValue(id, out var trainer);
        if (trainer != null)
        {
            // Добавляем клиентов тренера
            trainer.Clients = _clients.Values
                .Where(c => c.TrainerId == id)
                .ToList();
        }
        return Task.FromResult(trainer);
    }

    public Task AddAsync(Trainer trainer)
    {
        _trainers.TryAdd(trainer.Id, trainer);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Trainer trainer)
    {
        _trainers[trainer.Id] = trainer;
        return Task.CompletedTask;
    }
}