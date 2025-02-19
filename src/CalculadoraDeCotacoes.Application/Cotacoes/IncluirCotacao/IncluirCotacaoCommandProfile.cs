using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;

public class IncluirCotacaoCommandProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IncluirCotacaoCommand, Cotacao>()
            .Map(dest => dest.Segurado, src => new
            {
                src.NomeSegurado, src.Ddd, src.Telefone, src.Endereco, src.Cep, src.Documento, src.Premio,
                src.ImportanciaSegurada, src.DataNascimento
            })
            .Map(dest => dest.CotacoesBeneficiarios, src => src.Beneficiarios!.OrderBy(cb => cb.IdParentesco));

        config.NewConfig<Cotacao, IncluirCotacaoResult>();
    }
}