using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;

public class ListarBeneficiariosPorCotacaoQuery : RequisicaoPaginadaInputModel, IRequest<PaginatedList<ListarBeneficiariosPorCotacaoResult>>
{
    public int IdCotacao { get; set; }
}