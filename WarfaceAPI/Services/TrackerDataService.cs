using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Data;
using WarfaceAPI.Models;

namespace WarfaceAPI.Services;

/* Полный конструктор
    private readonly ApiClient _apiClient;
    private readonly IServiceProvider _serviceProvider;

    public TrackerDataService(ApiClient apiClient, IServiceProvider serviceProvider)
    {
        _apiClient = apiClient;
        _serviceProvider = serviceProvider;
    }
*/

// Краткий конструктор

public class TrackerDataService(ApiClient apiClient, IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    // Получение данных об игроке (Убийства, смерти, КД)
    public async Task<PlayerStats?> GetPlayerDataAsync(string nickname)
    {
        var url = $"http://api.warface.ru/user/stat/?name={nickname}";
        var apiResponse = await apiClient.SendApiRequestAsync<PlayerStats>(url);

        return apiResponse;
    }

    public async Task ChangePlayerDataAsync(string nickname)
    {
        var playerStats = await GetPlayerDataAsync(nickname);
        
        if (playerStats == null)
        {
            Console.WriteLine($"Игрок с ником {nickname} не найден.");
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Есть ли игрок в таблице PlayerStats
            var existingPlayer = await dbContext.PlayersStats.
                FirstOrDefaultAsync(p => p.UserId == playerStats.UserId);

            if (existingPlayer == null)
            {
                // Добавление в таблицу если игрока нет
                playerStats.LastChecked = DateTime.UtcNow;
                await dbContext.PlayersStats.AddAsync(playerStats);
            }
            else
            {
                // Сохранение текущие данные в историю
                var historyEntry = new PlayerStatsHistory
                {
                    UserId = existingPlayer.UserId,
                    Nickname = existingPlayer.Nickname,
                    PvpKills = existingPlayer.PvpKills,
                    PvpDeath = existingPlayer.PvpDeath,
                    PvpKd = existingPlayer.PvpKd,
                    PveKills = existingPlayer.PveKills,
                    PveDeath = existingPlayer.PveDeath,
                    PveKd = existingPlayer.PveKd,
                    Timestamp = existingPlayer.LastChecked
                };

                await dbContext.PlayerStatsHistories.AddAsync(historyEntry);
                
                // Обновляем текущие данные

                existingPlayer.PvpKills = playerStats.PvpKills;
                existingPlayer.PvpDeath = playerStats.PvpDeath;
                existingPlayer.PvpKd = playerStats.PvpKd;
                existingPlayer.PveKills = playerStats.PveKills;
                existingPlayer.PveDeath = playerStats.PveDeath;
                existingPlayer.PveKd = playerStats.PveKd;
                existingPlayer.LastChecked = DateTime.UtcNow;

                dbContext.PlayersStats.Update(existingPlayer);
            }

            await dbContext.SaveChangesAsync();
        }
    }
    
    public async Task<List<string>> GetUniqueNicknamesAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Получение уникальных ников из таблицы PlayerStatsHistories
            var nicknames = await dbContext.PlayersStats
                .Select(p => p.Nickname)
                .Distinct()
                .ToListAsync();

            return nicknames;
        }
    }
}