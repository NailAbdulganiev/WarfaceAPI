using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Models;

namespace WarfaceAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerStats> PlayersStats { get; set; }
    public DbSet<PlayerStatsHistory> PlayerStatsHistories { get; set; } 
}