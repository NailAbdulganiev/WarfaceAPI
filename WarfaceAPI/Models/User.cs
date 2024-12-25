namespace WarfaceAPI.Models;

public class User
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public int Experience { get; set; }
    public int Rank { get; set; }
    public int ClanId { get; set; }
    public string? ClanName { get; set; }
}