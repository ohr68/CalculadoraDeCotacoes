using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Entities;
using Mapster;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;

public class AlterarBeneficiarioProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BeneficiarioInputModel, CotacaoBeneficiario>();
    }
}