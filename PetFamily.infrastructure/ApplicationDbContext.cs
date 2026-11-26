using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PetFamily.infrastructure;

public class ApplicationDbContext : DbContext
{
    private ApplicationDbContext() {}
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString("VolunteerDb"))
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging();
    }
    
    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Warning)
            .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information));
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    
}