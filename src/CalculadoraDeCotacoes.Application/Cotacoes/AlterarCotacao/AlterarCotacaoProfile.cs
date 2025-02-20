using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Cotacoes.AlterarCotacao;

public class AlterarCotacaoProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AlterarCotacaoCommand, Cotacao>()
            .Map(dest => dest.Segurado, src => new
            {
                src.NomeSegurado, src.Ddd, src.Telefone, src.Endereco, src.Cep, src.Documento, src.Premio,
                src.ImportanciaSegurada, src.DataNascimento
            });
    }
}