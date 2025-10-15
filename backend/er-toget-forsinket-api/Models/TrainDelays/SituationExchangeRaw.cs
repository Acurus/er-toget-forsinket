using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace er_toget_forsinket_api.Models.TrainDelays;

public class SituationExchangeRaw
{
    [Key] public int Id { get; set; }

    [Column(TypeName = "TEXT")] public string RawMessage { get; set; } = null!;

    public DateTimeOffset ReceivedDateTime { get; set; } = DateTimeOffset.UtcNow;
}

public class SituationExchangeRawConfiguration : IEntityTypeConfiguration<SituationExchangeRaw>
{
    public void Configure(EntityTypeBuilder<SituationExchangeRaw> builder)
    {
        builder.ToTable("situation_exchange_raw", "train_delays");

        builder.Property(e => e.RawMessage)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(e => e.ReceivedDateTime)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("NOW()");
    }
}