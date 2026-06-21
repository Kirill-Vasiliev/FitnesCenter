using FitnesCenter.Domain.Entities;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Shared.Enums;
using FitnesCenter.Shared.Exceptions;

namespace FitnesCenter.Application.Services;

public class TrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
    {
        await _trainerRepository.AddAsync(trainer);
        return trainer;
    }

    public async Task<Trainer> UpdateTrainerAsync(Guid id, Trainer updated)
    {
        var existing = await _trainerRepository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException(nameof(Trainer), id);

        existing.Surname = updated.Surname;
        existing.Name = updated.Name;
        existing.Patronymic = updated.Patronymic;
        existing.Phone = updated.Phone;

        await _trainerRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task<Trainer> ChangeStatusAsync(Guid id, TrainerStatus newStatus)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id);
        if (trainer == null)
            throw new NotFoundException(nameof(Trainer), id);

        trainer.Status = newStatus;
        await _trainerRepository.UpdateAsync(trainer);
        return trainer;
    }

    public async Task<IEnumerable<Trainer>> GetAllTrainersAsync()
        => await _trainerRepository.GetAllAsync();

    public async Task<Trainer> GetTrainerDetailAsync(Guid id)
    {
        var trainer = await _trainerRepository.GetDetailAsync(id);
        return trainer ?? throw new NotFoundException(nameof(Trainer), id);
    }
}