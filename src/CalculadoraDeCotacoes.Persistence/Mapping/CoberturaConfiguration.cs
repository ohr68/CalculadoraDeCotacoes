using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class CoberturaConfiguration : IEntityTypeConfiguration<Cobertura>
{
    public void Configure(EntityTypeBuilder<Cobertura> builder)
    {
        builder.ToTable("Cobertura");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.Property(c => c.TipoCoberturaId)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(c => c.Valor)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne<TipoCobertura>(c => c.TipoCobertura)
            .WithMany(tc => tc.Coberturas)
            .HasForeignKey(c => c.TipoCoberturaId);
    }
}