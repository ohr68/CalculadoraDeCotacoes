using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class CotacaoCoberturaConfiguration : IEntityTypeConfiguration<CotacaoCobertura>
{
    public void Configure(EntityTypeBuilder<CotacaoCobertura> builder)
    {
        builder.ToTable("CotacaoCobertura");
        
        builder.HasKey(cc => cc.Id);

        builder.Property(cc => cc.IdCotacao)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(cc => cc.IdCobertura)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(cc => cc.ValorDesconto)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(cc => cc.ValorAgravo)
            .HasColumnType("decimal(18,2)");
        
        builder.Property(cc => cc.ValorTotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne<Cotacao>(cc => cc.Cotacao)
            .WithMany(c => c.CotacoesCoberturas)
            .HasForeignKey(cc => cc.IdCotacao);
        
        builder.HasOne<Cobertura>(cc => cc.Cobertura)
            .WithMany(c => c.CotacoesCoberturas)
            .HasForeignKey(cc => cc.IdCobertura);
    }
}