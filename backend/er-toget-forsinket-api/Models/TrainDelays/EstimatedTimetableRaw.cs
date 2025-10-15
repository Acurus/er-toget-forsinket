using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class EstimatedTimetableRaw
{
    [Key] public int Id { get; set; }

    [Column(TypeName = "TEXT")] public string RawMessage { get; set; } = null!;

    public DateTimeOffset ReceivedDateTime { get; set; } = DateTimeOffset.UtcNow;
}

public class EstimatedTimetableRawConfiguration : IEntityTypeConfiguration<EstimatedTimetableRaw>
{
    public void Configure(EntityTypeBuilder<EstimatedTimetableRaw> builder)
    {
        builder.ToTable("estimated_time_table_raw", "train_delays");

        builder.Property(e => e.RawMessage)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(e => e.ReceivedDateTime)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("NOW()");
    }
}