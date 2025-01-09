using Microsoft.AspNetCore.Mvc;
using WarfaceAPI.Services;

namespace WarfaceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PlayerStatsController(TrackerDataService trackerDataService) : ControllerBase
{
    [HttpGet("{nickname}")]
    public async Task<IActionResult> GetPlayerStats(string nickname)
    {
        // Получение текущей статистики
        var playerStats = await trackerDataService.GetPlayerDataAsync(nickname);

        if (playerStats == null)
        {
            return NotFound($"Игрок с ником {nickname} не найден.");
        }

        return Ok(playerStats);
    }

    [HttpPost("track/{nickname}")]
    public async Task<IActionResult> TrackPlayerStats(string nickname)
    {
        // Обновление данных игрока и сохранение изменений
        await trackerDataService.ChangePlayerDataAsync(nickname);

        return Ok($"Данные игрока {nickname} обновлены.");
    }
    
    
}