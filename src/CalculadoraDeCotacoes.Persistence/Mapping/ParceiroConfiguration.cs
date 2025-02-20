using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class ParceiroConfiguration : IEntityTypeConfiguration<Parceiro>
{
    public void Configure(EntityTypeBuilder<Parceiro> builder)
    {
        builder.ToTable("Parceiro");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Descricao)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.Property(p => p.Secret)
            .HasColumnType("varchar(50)")
            .IsRequired();
    }
}