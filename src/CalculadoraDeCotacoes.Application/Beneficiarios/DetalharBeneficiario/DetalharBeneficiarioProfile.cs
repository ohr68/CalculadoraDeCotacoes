using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;

public class DetalharBeneficiarioProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CotacaoBeneficiario, DetalharBeneficiarioResult>()
            .ConstructUsing(cb =>
                new DetalharBeneficiarioResult(cb.Id, cb.Nome, cb.Percentual, cb.TipoParentesco!.Descricao));
    }
}