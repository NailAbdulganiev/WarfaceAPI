using Microsoft.AspNetCore.Mvc;
using WarfaceAPI.Services;

namespace WarfaceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PlayerDataController(PlayerDataService playerDataService) : ControllerBase
{
    // Конструктор для контроллера на основе сервиса PlayerDataService

    [HttpPost("Save-PlayerData")]
    public async Task<IActionResult> SavePlayerData([FromQuery] string nickname)
    {
        try
        {
            await playerDataService.SavePlayerDataAsync(nickname);
            return Ok("Player data succesfully saved.");
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }
    
}