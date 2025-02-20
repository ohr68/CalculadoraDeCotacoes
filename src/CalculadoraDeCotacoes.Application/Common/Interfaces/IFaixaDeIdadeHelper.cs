using CalculadoraDeCotacoes.Domain.Entities;

namespace CalculadoraDeCotacoes.Application.Common.Interfaces;

public interface IFaixaDeIdadeHelper
{
    public Task<FaixaDeIdade> ObterFaixaDeIdadeDoSegurado(DateOnly dataNascimento, CancellationToken cancellationToken);
}