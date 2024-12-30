using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Models;

namespace WarfaceAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerStats> PlayersStats { get; set; }
    public DbSet<PlayerStatsHistory> PlayerStatsHistories { get; set; } 
}