using Newtonsoft.Json;

namespace WarfaceAPI.Models;

public class Player
{
    public int Id { get; set; }
    
    [JsonProperty("user_id")]
    public string? UserId { get; set; }
    
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }
    
    [JsonProperty("experience")]
    public int Experience { get; set; }
    
    [JsonProperty("rank_id")]
    public int Rank { get; set; }
    
    [JsonProperty("clan_id")]
    public int ClanId { get; set; }
    
    [JsonProperty("clan_name")]
    public string? ClanName { get; set; }
}