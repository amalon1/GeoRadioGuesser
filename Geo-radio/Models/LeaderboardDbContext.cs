using Microsoft.EntityFrameworkCore;

namespace Geo_radio.Models;

public class LeaderboardDbContext : DbContext {
    
    public LeaderboardDbContext(DbContextOptions<LeaderboardDbContext> options) : base(options) {}

    public DbSet<LeaderboardEntry> Leaderboard { get; set; }
}