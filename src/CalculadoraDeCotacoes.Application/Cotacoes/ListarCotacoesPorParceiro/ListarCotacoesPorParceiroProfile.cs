using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;

public class ListarCotacoesPorParceiroProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Cotacao, ListarCotacoesPorParceiroResult>()
            .ConstructUsing(c => new ListarCotacoesPorParceiroResult(c.Id, c.Segurado!.Nome, c.Segurado.Documento, c.Produto!.Descricao));
    }
}