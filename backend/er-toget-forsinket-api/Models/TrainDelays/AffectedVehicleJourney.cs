using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class AffectedVehicleJourney
{
    [Key]
    public int Id { get; set; }

    [Column("dated_vehicle_journey_ref")]
    public string? DatedVehicleJourneyRef { get; set; }

    [Column("stop_point_ref")]
    public string? StopPointRef { get; set; }

    [Column("stop_conditions")]
    public string? StopConditions { get; set; }

    // Foreign key
    [ForeignKey(nameof(SituationExchange))]
    [Column("situation_exchange_id")]
    public int SituationExchangeId { get; set; }

    public SituationExchange SituationExchange { get; set; } = null!;
}

public class AffectedVehicleJourneyConfiguration : IEntityTypeConfiguration<AffectedVehicleJourney>
{
    public void Configure(EntityTypeBuilder<AffectedVehicleJourney> builder)
    {
        builder.ToTable("affected_vehicle_journey", "train_delays");
    }
}