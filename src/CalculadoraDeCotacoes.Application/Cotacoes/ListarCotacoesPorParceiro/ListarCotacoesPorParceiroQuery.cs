using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;

public class ListarCotacoesPorParceiroQuery : RequisicaoPaginadaInputModel, IRequest<PaginatedList<ListarCotacoesPorParceiroResult>>
{
    public int IdParceiro { get; set; }
}