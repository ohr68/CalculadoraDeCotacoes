using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;

public class ExcluirCotacaoCommandHandler : IRequestHandler<ExcluirCotacaoCommand, ExcluirCotacaoResult>
{
    public async Task<ExcluirCotacaoResult> Handle(ExcluirCotacaoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}