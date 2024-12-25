using Newtonsoft.Json;
using WarfaceAPI.Models;

namespace WarfaceAPI.Services;

public class WarfaceApiService
{
    private readonly HttpClient _httpClient;

    public WarfaceApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> GetUserDataAsync(string username)
    {
        var url = $"http://api.warface.ru/user/stat/?name={username}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<WarfaceApiResponse>(content);

            if (apiResponse != null)
            {
                return new User
                {
                    UserId = apiResponse.UserId,
                    Username = apiResponse.Username,
                    Experience = apiResponse.Experience,
                    Rank = apiResponse.Rank,
                    ClanId = apiResponse.ClanId,
                    ClanName = apiResponse.ClanName
                };
            }
        }

        return null;
    }
}

public class WarfaceApiResponse
{
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [JsonProperty("nickname")]
    public string Username { get; set; }

    [JsonProperty("experience")]
    public int Experience { get; set; }

    [JsonProperty("rank_id")]
    public int Rank { get; set; }

    [JsonProperty("clan_id")]
    public int ClanId { get; set; }

    [JsonProperty("clan_name")]
    public string ClanName { get; set; }
        
}