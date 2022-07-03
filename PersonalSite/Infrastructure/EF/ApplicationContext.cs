using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Entities;

namespace PersonalSite.Infrastructure.EF;

public class ApplicationContext: DbContext
{
    private readonly string _connectionString;
    
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ProfileCredentialsEntity> ProfileCredentials { get; set; }

    public ApplicationContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }
}