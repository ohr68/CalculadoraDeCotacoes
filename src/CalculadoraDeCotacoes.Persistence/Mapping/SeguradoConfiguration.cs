using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraDeCotacoes.Persistence.Mapping;

public class SeguradoConfiguration : IEntityTypeConfiguration<Segurado>
{
    public void Configure(EntityTypeBuilder<Segurado> builder)
    {
        builder.ToTable("Segurado");

        builder.HasKey(s => s.CotacaoId);

        builder.Property(s => s.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(s => s.Ddd)
            .HasColumnType("int");

        builder.Property(s => s.Telefone)
            .HasColumnType("int");

        builder.Property(s => s.Endereco)
            .HasColumnType("varchar(300)")
            .IsRequired();

        builder.Property(s => s.Cep)
            .HasColumnType("varchar(8)")
            .IsRequired();

        builder.Property(s => s.Documento)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(s => s.DataNascimento)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(s => s.Premio)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(s => s.ImportanciaSegurada)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne<Cotacao>(s => s.Cotacao)
            .WithOne(c => c.Segurado)
            .HasForeignKey<Segurado>(s => s.CotacaoId);
    }
}