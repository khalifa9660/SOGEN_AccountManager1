using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Championship> Championships { get; set; }
    public DbSet<Team> Teams { get; set; }

}

