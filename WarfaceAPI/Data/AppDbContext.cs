using Microsoft.EntityFrameworkCore;
using WarfaceAPI.Models;

namespace WarfaceAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}