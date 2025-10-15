using er_toget_forsinket_api.Config.Constants;
using er_toget_forsinket_api.Models.TrainDelays;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace er_toget_forsinket_api.Repositories;

public class PostgresContext : IdentityDbContext<User>
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public DbSet<EstimatedTimetableRaw> EstimatedTimetableRaws { get; set; }
    public DbSet<SituationExchangeRaw> SituationExchangeRaws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(DatabaseSchemas.Auth);
        modelBuilder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);

        modelBuilder.ApplyConfiguration(new EstimatedTimetableRawConfiguration());
        modelBuilder.ApplyConfiguration(new SituationExchangeRawConfiguration());
        modelBuilder.ApplyConfiguration(new SituationExchangeConfiguration());
        modelBuilder.ApplyConfiguration(new AffectedVehicleJourneyConfiguration());

    }
}

public class User : IdentityUser
{
    public string? Initials { get; set; }
    public string? Theme { get; set; }
    public string? DisplayName { get; set; }
}