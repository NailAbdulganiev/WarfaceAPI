using Newtonsoft.Json;

namespace WarfaceAPI.Models;

public class PlayerStatsHistory
{
    public int Id { get; set; }
    
    [JsonProperty("user_id")]
    public string? UserId { get; set; }
    
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }
    
    [JsonProperty("kills")]
    public int? PvpKills { get; set; }
    
    [JsonProperty("death")]
    public int? PvpDeath { get; set; }
    
    [JsonProperty("pvp")]
    public float? PvpKd { get; set; }
    
    [JsonProperty("pve_kills")]
    public int? PveKills { get; set; }
    
    [JsonProperty("pve_death")]
    public int? PveDeath { get; set; }
    
    [JsonProperty("pve")]
    public float? PveKd { get; set; }
    public DateTime Timestamp { get; set; }
}