using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;

public class DetalharCotacaoQuery : IRequest<DetalharCotacaoResult>
{
    public int IdCotacao { get; set; }
}