using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;

public class ListarCoberturasPorCotacaoQuery : RequisicaoPaginadaInputModel, IRequest<PaginatedList<ListarCoberturasPorCotacaoResult>>
{
    public int IdCotacao { get; set; }
}