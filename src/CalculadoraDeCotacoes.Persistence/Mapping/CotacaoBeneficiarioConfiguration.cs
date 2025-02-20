using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class CotacaoBeneficiarioConfiguration : IEntityTypeConfiguration<CotacaoBeneficiario>
{
    public void Configure(EntityTypeBuilder<CotacaoBeneficiario> builder)
    {
        builder.ToTable("CotacaoBeneficiario");
        
        builder.HasKey(cb => cb.Id);
        
        builder.Property(cb => cb.IdCotacao)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(cb => cb.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(cb => cb.Percentual)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(cb => cb.IdParentesco)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne<Cotacao>(cb => cb.Cotacao)
            .WithMany(c => c.CotacoesBeneficiarios)
            .HasForeignKey(cb => cb.IdCotacao);

        builder.HasOne<TipoParentesco>(cb => cb.TipoParentesco)
            .WithMany(t => t.CotacoesBeneficiarios)
            .HasForeignKey(cb => cb.IdParentesco);
    }
}