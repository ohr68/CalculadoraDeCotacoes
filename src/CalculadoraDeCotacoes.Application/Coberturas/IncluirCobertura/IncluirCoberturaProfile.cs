using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Coberturas.IncluirCobertura;

public class IncluirCoberturaProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IncluirCoberturaCommand, CotacaoCobertura>();
    }
}