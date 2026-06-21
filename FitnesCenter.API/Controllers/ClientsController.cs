using Microsoft.AspNetCore.Mvc;
using FitnesCenter.API.DTOs;
using FitnesCenter.Application.Services;
using FitnesCenter.Domain.Entities;

namespace FitnesCenter.API.Controllers;

[ApiController]
[Route("api/clients")]
[Produces("application/json")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateDto dto)
    {
        var client = new Client
        {
            Surname = dto.Surname,
            Name = dto.Name,
            Patronymic = dto.Patronymic,
            Birthday = dto.Birthday,
            Phone = dto.Phone,
            Email = dto.Email,
            IsActive = true
        };

        var created = await _clientService.CreateClientAsync(client);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        return Ok(client);
    }

    /// <summary>
    /// Получить подробную информацию о клиенте
    /// </summary>
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var client = await _clientService.GetClientDetailAsync(id);
        return Ok(client);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClientCreateDto dto)
    {
        var updated = new Client
        {
            Surname = dto.Surname,
            Name = dto.Name,
            Patronymic = dto.Patronymic,
            Birthday = dto.Birthday,
            Phone = dto.Phone,
            Email = dto.Email
        };

        var client = await _clientService.UpdateClientAsync(id, updated);
        return Ok(client);
    }

    /// <summary>
    /// Активировать/деактивировать клиента
    /// </summary>
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        await _clientService.ToggleStatusAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Назначить тренера клиенту
    /// </summary>
    [HttpPost("{clientId}/trainer/{trainerId}")]
    public async Task<IActionResult> AssignTrainer(Guid clientId, Guid trainerId)
    {
        await _clientService.AssignTrainerAsync(clientId, trainerId);
        return NoContent();
    }
}