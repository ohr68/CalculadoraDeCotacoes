using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;

public class ExcluirCotacaoCommand : IRequest<ExcluirCotacaoResult>
{
    public int IdCotacao { get; set; }
}