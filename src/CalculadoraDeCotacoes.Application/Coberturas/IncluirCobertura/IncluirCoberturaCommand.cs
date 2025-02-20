using MediatR;

namespace CalculadoraDeCotacoes.Application.Coberturas.IncluirCobertura;

public class IncluirCoberturaCommand : IRequest<IncluirCoberturaResult>
{
    public int IdCotacao { get; set; }
    public int IdCobertura { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorAgravo { get; set; }
    public decimal ValorTotal { get; set; }
}