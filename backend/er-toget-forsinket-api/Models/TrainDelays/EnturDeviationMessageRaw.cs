using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using er_toget_forsinket_api.Config.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class EnturDeviationMessageRaw
{
    [Key] public int Id { get; set; }

    [Column(TypeName = "jsonb")] public string RawMessage { get; set; } = null!;

    public DateTimeOffset ReceivedDateTime { get; set; } = DateTimeOffset.UtcNow;
}

public class EnturDeviationMessageRawConfiguration : IEntityTypeConfiguration<EnturDeviationMessageRaw>
{
    public void Configure(EntityTypeBuilder<EnturDeviationMessageRaw> builder)
    {
        builder.ToTable("entur_delay_messages_raw", "train_delays");

        builder.Property(e => e.RawMessage)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(e => e.ReceivedDateTime)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("NOW()");
    }
}