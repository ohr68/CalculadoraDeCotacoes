using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Persistence.Context;

namespace CalculadoraDeCotacoes.Persistence.Configuration;

public class DbInitializer
{
    public static void SeedDatabase(ApplicationDbContext context)
    {
        if (!context.Produtos.Any())
        {
            var produtos = new List<Produto>
            {
                new() { Id = 1, Descricao = "Vida Starter", ValorBase = 10M, Limite = 10_000M },
                new() { Id = 2, Descricao = "Vida AP+", ValorBase = 12.5M, Limite = 20_000M },
                new() { Id = 3, Descricao = "Vida Plus Master", ValorBase = 20M, Limite = 100_000M },
                new() { Id = 4, Descricao = "Vida Galaxy Membership", ValorBase = 4_500M, Limite = 10_000_000M },
            };

            context.Produtos.AddRange(produtos);
            context.SaveChanges();
        }

        if (!context.Parceiros.Any())
        {
            var parceiros = new List<Parceiro>
            {
                new() { Id = 1, Descricao = "Lojas Jackellino", Secret = "XPTO2" },
                new() { Id = 2, Descricao = "Lojas Rede Cusco de La Rocha", Secret = "IDKFA" },
                new() { Id = 3, Descricao = "2 Irmãos Global Membership Traders", Secret = "IDDQD" }
            };

            context.Parceiros.AddRange(parceiros);
            context.SaveChanges();
        }

        if (!context.TiposParentesco.Any())
        {
            var tiposParentesco = new List<TipoParentesco>
            {
                new() { Id = 1, Descricao = "Mãe" },
                new() { Id = 2, Descricao = "Pai" },
                new() { Id = 3, Descricao = "Cônjuge" },
                new() { Id = 4, Descricao = "Filho(a)" },
                new() { Id = 5, Descricao = "Outros" },
            };

            context.TiposParentesco.AddRange(tiposParentesco);
            context.SaveChanges();
        }

        if (!context.TiposCobertura.Any())
        {
            var tiposCobertura = new List<TipoCobertura>
            {
                new() { Id = 1, Descricao = "Básica" },
                new() { Id = 2, Descricao = "Adicional" }
            };

            context.TiposCobertura.AddRange(tiposCobertura);
            context.SaveChanges();
        }

        if (!context.Coberturas.Any())
        {
            var coberturas = new List<Cobertura>
            {
                new() { Id = 1, Descricao = "Morte Acidental", TipoCoberturaId = 1, Valor = 40M },
                new() { Id = 2, Descricao = "Morte Qualquer Causa", TipoCoberturaId = 1, Valor = 36.5M },
                new() { Id = 3, Descricao = "Invalidez Parcial ou Total", TipoCoberturaId = 1, Valor = 28.95M },
                new() { Id = 4, Descricao = "Assistência Funeral", TipoCoberturaId = 2, Valor = 18.96M },
                new() { Id = 5, Descricao = "Assistência Odontológica", TipoCoberturaId = 2, Valor = 12.55M },
                new() { Id = 6, Descricao = "Assistência PET", TipoCoberturaId = 2, Valor = 15.33M }
            };

            context.Coberturas.AddRange(coberturas);
            context.SaveChanges();
        }

        if (!context.FaixasDeIdade.Any())
        {
            var faixasDeIdade = new List<FaixaDeIdade>
            {
                new() { Id = 1, Descricao = "6 a 18 Anos", Desconto = 20, Agravo = 0 },
                new() { Id = 2, Descricao = "19 a 25 Anos", Desconto = 10, Agravo = 0 },
                new() { Id = 3, Descricao = "26 a 35 Anos", Desconto = 0, Agravo = 0 },
                new() { Id = 4, Descricao = "36 a 42 Anos", Desconto = 0, Agravo = 20 },
                new() { Id = 5, Descricao = "43 a 65 Anos", Desconto = 0, Agravo = 40 }
            };
            
            context.FaixasDeIdade.AddRange(faixasDeIdade);
            context.SaveChanges();
        }
    }
}