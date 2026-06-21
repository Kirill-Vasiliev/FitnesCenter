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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        return Ok(client);
    }

    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var client = await _clientService.GetClientDetailAsync(id);
        return Ok(client);
    }

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

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        await _clientService.ToggleStatusAsync(id);
        return NoContent();
    }

    [HttpPost("{clientId}/trainer/{trainerId}")]
    public async Task<IActionResult> AssignTrainer(Guid clientId, Guid trainerId)
    {
        await _clientService.AssignTrainerAsync(clientId, trainerId);
        return NoContent();
    }

    // ???????????????????????????????????????????????????????????????
    // НОВЫЕ МЕТОДЫ ДЛЯ ЧАСТИ 2
    // ???????????????????????????????????????????????????????????????

    /// <summary>
    /// Назначить шкафчик клиенту
    /// </summary>
    [HttpPost("{clientId}/locker/{lockerId}")]
    public async Task<IActionResult> AssignLocker(Guid clientId, Guid lockerId)
    {
        await _clientService.AssignLockerAsync(clientId, lockerId);
        return NoContent();
    }

    /// <summary>
    /// Добавить услугу клиенту
    /// </summary>
    [HttpPost("{clientId}/additionalServices/{serviceId}")]
    public async Task<IActionResult> AddService(Guid clientId, string serviceId)
    {
        await _clientService.AddServiceAsync(clientId, serviceId);
        return NoContent();
    }
}