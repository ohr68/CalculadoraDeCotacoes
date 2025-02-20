using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class TipoCoberturaConfiguration : IEntityTypeConfiguration<TipoCobertura>
{
    public void Configure(EntityTypeBuilder<TipoCobertura> builder)
    {
        builder.ToTable("TipoCobertura");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .ValueGeneratedNever();
        
        builder.Property(t => t.Descricao)
            .HasColumnType("varchar(50)")
            .IsRequired();
    }
}