using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Infrastructure.EF;

public class ApplicationContext: DbContext
{
    private readonly string _connectionString;
    
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ProfileCredentialsEntity> ProfileCredentials { get; set; }
    public DbSet<GoogleProfileEntity> GoogleProfiles { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<FileObjectEntity> Posts { get; set; }
    public DbSet<FileObjectChangeEntity> PostChanges { get; set; }

    public ApplicationContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        base.OnConfiguring(optionsBuilder);
    }
}