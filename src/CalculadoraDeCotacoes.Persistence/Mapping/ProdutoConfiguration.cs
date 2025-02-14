using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Descricao)
            .HasColumnType("varchar(300)")
            .IsRequired();
        
        builder.Property(p => p.ValorBase)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(p => p.Limite)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();
    }
}