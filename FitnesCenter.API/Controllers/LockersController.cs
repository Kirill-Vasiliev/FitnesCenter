using Microsoft.AspNetCore.Mvc;
using FitnesCenter.Domain.Interfaces;

namespace FitnesCenter.API.Controllers;

[ApiController]
[Route("api/lockers")]
[Produces("application/json")]
public class LockersController : ControllerBase
{
    private readonly ILockerRepository _lockerRepository;

    public LockersController(ILockerRepository lockerRepository)
    {
        _lockerRepository = lockerRepository;
    }

    /// <summary>
    /// Получить список всех шкафчиков со статусом (свободен / занят)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lockers = await _lockerRepository.GetAllAsync();
        return Ok(lockers);
    }
}