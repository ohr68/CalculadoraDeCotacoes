using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class TipoParentescoConfiguration : IEntityTypeConfiguration<TipoParentesco>
{
    public void Configure(EntityTypeBuilder<TipoParentesco> builder)
    {
        builder.ToTable("TipoParentesco");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Descricao)
            .HasColumnType("varchar(60)")
            .IsRequired();
    }
}