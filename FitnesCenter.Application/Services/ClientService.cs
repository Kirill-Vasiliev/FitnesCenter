using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Shared.Exceptions;

namespace FitnesCenter.Application.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly ILockerRepository _lockerRepository;
    private readonly IServiceRepository _serviceRepository;

    public ClientService(
        IClientRepository clientRepository,
        ITrainerRepository trainerRepository,
        ILockerRepository lockerRepository,
        IServiceRepository serviceRepository)
    {
        _clientRepository = clientRepository;
        _trainerRepository = trainerRepository;
        _lockerRepository = lockerRepository;
        _serviceRepository = serviceRepository;
    }


    // МЕТОДЫ из Части 1
   

    public async Task<Client> CreateClientAsync(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.Surname))
            throw new ValidationException("Фамилия обязательна");

        if (string.IsNullOrWhiteSpace(client.Name))
            throw new ValidationException("Имя обязательна");

        await _clientRepository.AddAsync(client);
        return client;
    }

    public async Task<Client> UpdateClientAsync(Guid id, Client updated)
    {
        var existing = await _clientRepository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException(nameof(Client), id);

        existing.Surname = updated.Surname;
        existing.Name = updated.Name;
        existing.Patronymic = updated.Patronymic;
        existing.Birthday = updated.Birthday;
        existing.Phone = updated.Phone;
        existing.Email = updated.Email;

        await _clientRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
        => await _clientRepository.GetAllAsync();

    public async Task<Client> GetClientAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client ?? throw new NotFoundException(nameof(Client), id);
    }

    public async Task<Client> GetClientDetailAsync(Guid id)
    {
        var client = await _clientRepository.GetDetailAsync(id);
        return client ?? throw new NotFoundException(nameof(Client), id);
    }

    public async Task ToggleStatusAsync(Guid id)
    {
        var client = await GetClientAsync(id);
        client.IsActive = !client.IsActive;
        await _clientRepository.UpdateAsync(client);
    }

    public async Task AssignTrainerAsync(Guid clientId, Guid trainerId)
    {
        var client = await GetClientAsync(clientId);

        var trainer = await _trainerRepository.GetByIdAsync(trainerId);
        if (trainer == null)
            throw new NotFoundException(nameof(Trainer), trainerId);

        client.TrainerId = trainerId;
        await _clientRepository.UpdateAsync(client);
    }

   

    public async Task AssignLockerAsync(Guid clientId, Guid lockerId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
            throw new NotFoundException(nameof(Client), clientId);

        var locker = await _lockerRepository.GetByIdAsync(lockerId);
        if (locker == null)
            throw new NotFoundException(nameof(Locker), lockerId);

        if (locker.ClientId != null)
            throw new ConflictException("Шкафчик уже занят");

        if (client.LockerId != null)
        {
            var oldLocker = await _lockerRepository.GetByIdAsync(client.LockerId.Value);
            if (oldLocker != null)
            {
                oldLocker.ClientId = null;
                await _lockerRepository.UpdateAsync(oldLocker);
            }
        }

        locker.ClientId = clientId;
        client.LockerId = lockerId;
        await _lockerRepository.UpdateAsync(locker);
        await _clientRepository.UpdateAsync(client);
    }

    public async Task AddServiceAsync(Guid clientId, string serviceId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
            throw new NotFoundException(nameof(Client), clientId);

        var service = await _serviceRepository.GetByIdAsync(serviceId);
        if (service == null)
            throw new NotFoundException(nameof(Service), serviceId);

        var clientDetail = await _clientRepository.GetDetailAsync(clientId);
        if (clientDetail!.Services.Any(s => s.Id == serviceId))
            throw new ConflictException("Услуга уже добавлена клиенту");

        client.Services.Add(service);
        await _clientRepository.UpdateAsync(client);
    }
}