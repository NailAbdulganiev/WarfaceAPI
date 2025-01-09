using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Data;
using WarfaceAPI.Models;
using Microsoft.Extensions.Logging;

namespace WarfaceAPI.Controllers;

// Получение данных из таблицы для построения web-страницы для графического отображения истории статистики игроков.
[ApiController]
[Route("api/[controller]")]
public class PlayerStatsQueryController(AppDbContext appDbContext, ILogger<PlayerStatsQueryController> logger)
    : ControllerBase
{
    [HttpGet("stats/{nickname}")]
    public async Task<IActionResult> GetPlayerStatsForCharts(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
            logger.LogWarning("Пустой никнейм получен в запросе.");
            return BadRequest("Никнейм не может быть пустым.");
        }

        var stats = await appDbContext.PlayerStatsHistories
            .Where(p => p.Nickname == nickname)
            .OrderBy(p => p.Timestamp)
            .Select(p => new
            { 
                Timestamp = p.Timestamp.AddHours(4).ToString("yyyy-MM-dd HH:mm:ss"),
                PveKills = p.PveKills ?? 0,
                PveDeaths = p.PveDeath ?? 0,
                PveKd = p.PveKd ?? 0,
                PvpKills = p.PvpKills ?? 0,
                PvpDeaths = p.PvpDeath ?? 0,
                PvpKd = p.PvpKd ?? 0
                
            })
            .ToListAsync();
        
        if (!stats.Any())
        {
            logger.LogInformation("Нет статистики для игрока {Nickname}.", nickname);
            return NotFound($"Нет статистики для игрока {nickname} в таблице.");
        }

        return Ok(stats);
    }
}