using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Maqui.Domain.Entities;
using Maqui.Domain.Enums;

namespace Maqui.Infrastructure.Configurations;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes", t =>
        {
            t.HasCheckConstraint("CK_TipoDocumento",
                "[TipoDocumento] IN ('DNI', 'RUC', 'CarnetExtranjeria')");
        });

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Nombres)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Apellidos)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.FechaNacimiento)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(c => c.TipoDocumento)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<TipoDocumento>(v))
            .HasMaxLength(50);

        builder.Property(c => c.NumeroDocumento)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.RutaHojaVida)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.RutaFoto)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.EsActivo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.FechaCreacion)
            .IsRequired()
            .HasColumnType("datetime2(7)");

        builder.Property(c => c.FechaModificacion)
            .HasColumnType("datetime2(7)");

        builder.HasIndex(c => c.NumeroDocumento)
            .IsUnique()
            .HasDatabaseName("UQ_NumeroDocumento");

        builder.HasIndex(c => c.EsActivo)
            .HasDatabaseName("IX_Clientes_EsActivo");
    }
}