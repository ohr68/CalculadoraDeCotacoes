using CalculadoraDeCotacoes.Application.Dto;
using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;

public class DetalharCotacaoProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Cotacao, DetalharCotacaoResult>()
            .Map(dest => dest.Segurado, src => src.Segurado)
            .Map(dest => dest.Produto, src => src.Produto)
            .Map(dest => dest.Beneficiarios, src => src.CotacoesBeneficiarios)
            .Map(dest => dest.Coberturas, src => src.CotacoesCoberturas);

        config.NewConfig<Segurado, SeguradoDto>();
        config.NewConfig<Produto, ProdutoDto>();
        config.NewConfig<CotacaoBeneficiario, CotacaoBeneficiarioDto>();
        config.NewConfig<CotacaoCobertura, CotacaoCoberturaDto>();
    }
}