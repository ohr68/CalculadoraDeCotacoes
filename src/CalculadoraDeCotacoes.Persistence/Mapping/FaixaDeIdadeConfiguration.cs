using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class FaixaDeIdadeConfiguration : IEntityTypeConfiguration<FaixaDeIdade>
{
    public void Configure(EntityTypeBuilder<FaixaDeIdade> builder)
    {
        builder.ToTable("FaixaDeIdade");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedNever();

        builder.Property(f => f.Descricao)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(f => f.Desconto)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(f => f.Agravo)
            .HasColumnType("int")
            .IsRequired();
    }
}