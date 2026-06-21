using Microsoft.AspNetCore.Mvc;
using FitnesCenter.API.DTOs;
using FitnesCenter.Application.Services;
using FitnesCenter.Domain.Entities;
using FitnesCenter.Shared.Enums;

namespace FitnesCenter.API.Controllers;

[ApiController]
[Route("api/trainers")]
[Produces("application/json")]
public class TrainersController : ControllerBase
{
    private readonly TrainerService _trainerService;

    public TrainersController(TrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    /// <summary>
    /// Создать нового тренера
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TrainerCreateDto dto)
    {
        var trainer = new Trainer
        {
            Surname = dto.Surname,
            Name = dto.Name,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone,
            Status = TrainerStatus.WORKING // По умолчанию
        };

        var created = await _trainerService.CreateTrainerAsync(trainer);
        return CreatedAtAction(nameof(GetDetail), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные тренера
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TrainerCreateDto dto)
    {
        var updated = new Trainer
        {
            Surname = dto.Surname,
            Name = dto.Name,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone
        };

        var trainer = await _trainerService.UpdateTrainerAsync(id, updated);
        return Ok(trainer);
    }

    /// <summary>
    /// Изменить статус тренера
    /// </summary>
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] TrainerStatusUpdateDto dto)
    {
        // Преобразуем строку в enum
        if (!Enum.TryParse<TrainerStatus>(dto.Status, true, out var status))
        {
            return BadRequest(new { error = "Некорректный статус. Допустимые значения: WORKING, ON_LEAVE, NOT_WORKING" });
        }

        var trainer = await _trainerService.ChangeStatusAsync(id, status);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить подробную информацию о тренере (со списком клиентов)
    /// </summary>
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var trainer = await _trainerService.GetTrainerDetailAsync(id);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить список всех тренеров
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trainers = await _trainerService.GetAllTrainersAsync();
        return Ok(trainers);
    }
}