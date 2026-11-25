using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PetFamily.infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.IsMappedToJson())
            {
                var containerColumn = entity.GetContainerColumnName();
                if (!string.IsNullOrEmpty(containerColumn))
                    entity.SetContainerColumnName(ToSnakeCase(containerColumn));

                foreach (var property in entity.GetProperties())
                {
                    var jsonName = property.GetJsonPropertyName();
                    if (!string.IsNullOrEmpty(jsonName))
                        property.SetJsonPropertyName(ToSnakeCase(jsonName));
                }
                continue;
            }

            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
                entity.SetTableName(ToSnakeCase(tableName));

            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName();
                if (!string.IsNullOrEmpty(columnName))
                    property.SetColumnName(ToSnakeCase(columnName));
            }

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName))
                    key.SetName(ToSnakeCase(keyName));
            }

            foreach (var fk in entity.GetForeignKeys())
            {
                var fkName = fk.GetConstraintName();
                if (!string.IsNullOrEmpty(fkName))
                    fk.SetConstraintName(ToSnakeCase(fkName));
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexName))
                    index.SetDatabaseName(ToSnakeCase(indexName));
            }
        }
    }
    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Warning)
            .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information));

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}