using Microsoft.AspNetCore.Mvc;
using WarfaceAPI.Services;

namespace WarfaceAPI.Controllers;

[ApiController] 
[Route("api/[controller]")]

public class Warfacecontroller :ControllerBase
{
    private readonly UserService _userService;

    public Warfacecontroller(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("save-user")]
    public async Task<IActionResult> SaveUser([FromQuery] string username)
    {
        try
        {
            await _userService.SaveUserDataAsync(username);
            return Ok("User data succesfully saved");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}