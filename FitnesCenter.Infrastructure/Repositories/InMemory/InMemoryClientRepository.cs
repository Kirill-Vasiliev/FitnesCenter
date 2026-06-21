using System.Collections.Concurrent;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;

namespace FitnesCenter.Infrastructure.Repositories.InMemory;

/// <summary>
/// In-Memory реализация репозитория клиентов
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    // ConcurrentDictionary для потокобезопасности
    private readonly ConcurrentDictionary<Guid, Client> _clients = new();
    private readonly ConcurrentDictionary<Guid, Trainer> _trainers = new();

    public InMemoryClientRepository()
    {
        //тестовые данные
        SeedData();
    }

    private void SeedData()
    {
        //тестовый тренер
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

        //тестовый клиент
        var client1 = new Client
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Surname = "Петров",
            Name = "Иван",
            Patronymic = "Алексеевич",
            Birthday = new DateOnly(1990, 5, 15),
            Phone = "+7-999-222-22-22",
            Email = "ivan@example.com",
            IsActive = true,
            TrainerId = trainer1.Id
        };
        _clients.TryAdd(client1.Id, client1);

        // клиент без тренера
        var client2 = new Client
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
        };
        _clients.TryAdd(client2.Id, client2);
    }

    public Task<IEnumerable<Client>> GetAllAsync()
    {
        return Task.FromResult(_clients.Values.AsEnumerable());
    }

    public Task<Client?> GetByIdAsync(Guid id)
    {
        _clients.TryGetValue(id, out var client);
        return Task.FromResult(client);
    }

    public Task<Client?> GetDetailAsync(Guid id)
    {
        _clients.TryGetValue(id, out var client);
        if (client?.TrainerId != null)
        {
            _trainers.TryGetValue(client.TrainerId.Value, out var trainer);
            client.Trainer = trainer;
        }
        return Task.FromResult(client);
    }

    public Task AddAsync(Client client)
    {
        _clients.TryAdd(client.Id, client);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Client client)
    {
        _clients[client.Id] = client;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _clients.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}