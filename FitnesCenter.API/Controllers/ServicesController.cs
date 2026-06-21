using Microsoft.AspNetCore.Mvc;
using FitnesCenter.Domain.Interfaces;

namespace FitnesCenter.API.Controllers;

[ApiController]
[Route("api/additionalServices")]
[Produces("application/json")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;

    public ServicesController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    /// <summary>
    /// Получить список всех услуг с ценами
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceRepository.GetAllAsync();
        return Ok(services);
    }

    /// <summary>
    /// Получить информацию об услуге + список подписанных клиентов
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null)
            return NotFound(new { error = $"Услуга с ID '{id}' не найдена" });
        return Ok(service);
    }
}