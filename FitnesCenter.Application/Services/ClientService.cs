using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Shared.Exceptions;

namespace FitnesCenter.Application.Services;

/// <summary>
/// Бизнес-логика для работы с клиентами
/// </summary>
public class ClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ITrainerRepository _trainerRepository;

    public ClientService(
        IClientRepository clientRepository,
        ITrainerRepository trainerRepository)
    {
        _clientRepository = clientRepository;
        _trainerRepository = trainerRepository;
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    public async Task<Client> CreateClientAsync(Client client)
    {
        // Бизнес-валидация
        if (string.IsNullOrWhiteSpace(client.Surname))
            throw new ValidationException("Фамилия обязательна");

        if (string.IsNullOrWhiteSpace(client.Name))
            throw new ValidationException("Имя обязательно");

        await _clientRepository.AddAsync(client);
        return client;
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    public async Task<Client> UpdateClientAsync(Guid id, Client updated)
    {
        var existing = await _clientRepository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException(nameof(Client), id);

        // Обновляем только разрешённые поля
        existing.Surname = updated.Surname;
        existing.Name = updated.Name;
        existing.Patronymic = updated.Patronymic;
        existing.Birthday = updated.Birthday;
        existing.Phone = updated.Phone;
        existing.Email = updated.Email;

        await _clientRepository.UpdateAsync(existing);
        return existing;
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
        => await _clientRepository.GetAllAsync();

    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    public async Task<Client> GetClientAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client ?? throw new NotFoundException(nameof(Client), id);
    }

    /// <summary>
    /// Получить подробную информацию о клиенте
    /// </summary>
    public async Task<Client> GetClientDetailAsync(Guid id)
    {
        var client = await _clientRepository.GetDetailAsync(id);
        return client ?? throw new NotFoundException(nameof(Client), id);
    }

    /// <summary>
    /// Активировать/деактивировать клиента
    /// </summary>
    public async Task ToggleStatusAsync(Guid id)
    {
        var client = await GetClientAsync(id);
        client.IsActive = !client.IsActive;
        await _clientRepository.UpdateAsync(client);
    }

    /// <summary>
    /// Назначить тренера клиенту
    /// </summary>
    public async Task AssignTrainerAsync(Guid clientId, Guid trainerId)
    {
        var client = await GetClientAsync(clientId);

        var trainer = await _trainerRepository.GetByIdAsync(trainerId);
        if (trainer == null)
            throw new NotFoundException(nameof(Trainer), trainerId);

        client.TrainerId = trainerId;
        await _clientRepository.UpdateAsync(client);
    }
}