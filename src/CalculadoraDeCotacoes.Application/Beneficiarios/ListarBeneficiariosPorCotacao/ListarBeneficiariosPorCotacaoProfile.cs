using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;

public class ListarBeneficiariosPorCotacaoProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CotacaoBeneficiario, ListarBeneficiariosPorCotacaoResult>()
            .ConstructUsing(cb =>
                new ListarBeneficiariosPorCotacaoResult(cb.Nome, cb.TipoParentesco!.Descricao, cb.Percentual));
    }
}