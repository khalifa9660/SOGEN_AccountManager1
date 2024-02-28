using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }

    public DbSet<Team> Teams { get; set; }
}

