using WarfaceAPI.Data;
using WarfaceAPI.Models;

namespace WarfaceAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly WarfaceApiService _apiService;

    public UserService(AppDbContext context, WarfaceApiService apiService)
    {
        _context = context;
        _apiService = apiService;
    }

    public async Task SaveUserDataAsync(string username)
    {
        var userData = await _apiService.GetUserDataAsync(username);

        if (userData != null)
        {
            // Проверяем, существует ли пользователь в базе
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == userData.UserId);

            if (existingUser == null)
            {
                _context.Users.Add(userData); // Добавляем нового пользователя
            }
            else
            {
                // Обновляем существующего пользователя
                existingUser.Username = userData.Username;
                existingUser.Experience = userData.Experience;
                existingUser.Rank = userData.Rank;
                existingUser.ClanId = userData.ClanId;
                existingUser.ClanName = userData.ClanName;
            }

            await _context.SaveChangesAsync();
        }
    }
}