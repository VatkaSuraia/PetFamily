using Microsoft.EntityFrameworkCore;
using PetFamily.Domain;

namespace PetFamily.infrastructure;

public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadDbContext).Assembly);
}