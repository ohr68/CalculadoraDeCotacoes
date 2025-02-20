using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;

public class ListarCoberturasPorCotacaoProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CotacaoCobertura, ListarCoberturasPorCotacaoResult>();
    }
}