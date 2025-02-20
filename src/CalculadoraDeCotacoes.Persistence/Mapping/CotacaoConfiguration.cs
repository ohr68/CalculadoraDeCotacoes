using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class CotacaoConfiguration : IEntityTypeConfiguration<Cotacao>
{
    public void Configure(EntityTypeBuilder<Cotacao> builder)
    {
        builder.ToTable("Cotacao");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.IdProduto)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(c => c.DataCriacao)
            .HasColumnType("datetime2(7)")
            .IsRequired();

        builder.Property(c => c.DataAtualizacao)
            .HasColumnType("datetime2(7)");

        builder.Property(c => c.IdParceiro)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne<Produto>(c => c.Produto)
            .WithMany(p => p.Cotacoes)
            .HasForeignKey(c => c.IdProduto);
        
        builder.HasOne<Parceiro>(c => c.Parceiro)
            .WithMany(p => p.Cotacoes)
            .HasForeignKey(c => c.IdParceiro);
    }
}