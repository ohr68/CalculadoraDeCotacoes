using CalculadoraDeCotacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Cobertura> Coberturas { get; set; }
    public DbSet<Cotacao> Cotacoes { get; set; }
    public DbSet<CotacaoBeneficiario> CotacoesBeneficiarios { get; set; }
    public DbSet<CotacaoCobertura> CotacoesCoberturas { get; set; }
    public DbSet<FaixaDeIdade> FaixasDeIdade { get; set; }
    public DbSet<Parceiro> Parceiros { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Segurado> Segurados { get; set; }
    public DbSet<TipoCobertura> TiposCobertura { get; set; }
    public DbSet<TipoParentesco> TiposParentesco { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}