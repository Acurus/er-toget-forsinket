using er_toget_forsinket_api.Models.TrainDelays;
using Microsoft.EntityFrameworkCore;

namespace er_toget_forsinket_api.Repositories;

// Inherit from DbContext
public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public DbSet<EstimatedTimetableRaw> EstimatedTimetableRaws { get; set; }
    public DbSet<SituationExchangeRaw> SituationExchangeRaws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EstimatedTimetableRawConfiguration());
        modelBuilder.ApplyConfiguration(new SituationExchangeRawConfiguration());
        modelBuilder.ApplyConfiguration(new SituationExchangeConfiguration());
        modelBuilder.ApplyConfiguration(new AffectedVehicleJourneyConfiguration());
    }
}