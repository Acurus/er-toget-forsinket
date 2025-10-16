using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class SituationExchange
{
    [Key] public int Id { get; set; }

    [Column("creation_time")] public string? CreationTime { get; set; }

    [Column("participant_ref")] public string? ParticipantRef { get; set; }

    [Column("priority")] public int? Priority { get; set; }

    [Column("situation_number")] public string? SituationNumber { get; set; }
    [Column("progress")] public string? Progress { get; set; }
    [Column("report_type")] public string? ReportType { get; set; }

    [Column("version")] public string? Version { get; set; }
    [Column("start_time")] public string? StartTime { get; set; }
    [Column("end_time")]public string? EndTime { get; set; }

    [Column("versioned_at_time")] public string? VersionedAtTime { get; set; }

    [Column("summary_no")] public string? SummaryNo { get; set; }
    [Column("description_no")] public string? DescriptionNo { get; set; }

    // Navigation property
    public List<AffectedStopPointEntry> AffectedStopPoints { get; set; } = new();
}

public class SituationExchangeConfiguration : IEntityTypeConfiguration<SituationExchange>
{
    public void Configure(EntityTypeBuilder<SituationExchange> builder)
    {
        builder.ToTable("situation_exchange", "train_delays");
    }
}