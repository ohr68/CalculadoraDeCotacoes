using CalculadoraDeCotacoes.Application.Common.Interfaces;
using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Common;

public class FaixaDeIdadeHelper(ApplicationDbContext context) : IFaixaDeIdadeHelper
{
    public async Task<FaixaDeIdade> ObterFaixaDeIdadeDoSegurado(DateOnly dataNascimento,
        CancellationToken cancellationToken)
    {
        var idade = CalculadoraDeIdade.ObterIdade(dataNascimento);
        var faixasDeIdadeExistentes = await context.FaixasDeIdade
            .ToListAsync(cancellationToken);

        var idFaixaDeIdade = faixasDeIdadeExistentes
            .AsEnumerable()
            .Select(f => new
            {
                Id = f.Id,
                Range = f.Descricao!.Replace("Anos", string.Empty).Split('a',
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            })
            .Where(f => f.Range.Length == 2 &&
                        int.TryParse(f.Range[0].Trim(), out var min) &&
                        int.TryParse(f.Range[1].Trim(), out var max) &&
                        idade >= min && idade <= max)
            .Select(f => f.Id)
            .FirstOrDefault();

        if (idFaixaDeIdade == 0)
            throw new ValidationException("Cotação recusada.\nMotivo:Faixa de idade fora do permitido.");

        return faixasDeIdadeExistentes.Single(f => f.Id == idFaixaDeIdade);
    }
}