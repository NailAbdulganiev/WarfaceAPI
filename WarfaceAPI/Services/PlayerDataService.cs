using WarfaceAPI.Models;

namespace WarfaceAPI.Services;

/* Полная запись конструктора
...
private readonly ApiClient _apiClient;
    
    public PlayerDataService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
...
*/ 

// Краткая запись конструктора
public class PlayerDataService(ApiClient apiClient)
{
    // Сохранение данных игрока (Ник, опыт, ранг, клан) в таблицу Players класса Player с помощью контроллера PlayerData
    public async Task<Player?> SavePlayerDataAsync(string nickname)
    {
        var url = $"http://api.warface.ru/user/stat/?name={nickname}";
        var apiResponse = await apiClient.SendApiRequestAsync<Player>(url);

        return apiResponse;
    }
}