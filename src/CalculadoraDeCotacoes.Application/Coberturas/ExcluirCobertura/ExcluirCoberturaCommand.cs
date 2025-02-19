using MediatR;

namespace CalculadoraDeCotacoes.Application.Coberturas.ExcluirCobertura;

public class ExcluirCoberturaCommand : IRequest<ExcluirCoberturaResult>
{
    public int IdCotacao { get; set; }
    public int IdCobertura { get; set; }
}