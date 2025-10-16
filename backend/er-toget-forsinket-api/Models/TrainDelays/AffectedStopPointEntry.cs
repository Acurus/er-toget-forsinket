using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class AffectedStopPointEntry
{
    [Key]
    public int Id { get; set; }
   
    [Column("dated_vehicle_journey_ref")] public string? DatedVehicleJourneyRef { get; set; }
    [Column("stop_point_ref")] public string? StopPointRef { get; set; }
    [Column("stop_conditions")] public List<string> StopConditions { get; set; } = new();
}

public class AffectedStopPointEntryConfiguration : IEntityTypeConfiguration<AffectedStopPointEntry>
{
    public void Configure(EntityTypeBuilder<AffectedStopPointEntry> builder)
    {
        builder.ToTable("affected_stop_point", "train_delays");
    }
}