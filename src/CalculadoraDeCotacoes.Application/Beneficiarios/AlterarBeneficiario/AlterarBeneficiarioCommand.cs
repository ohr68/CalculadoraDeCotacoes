using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;

public class AlterarBeneficiarioCommand : IRequest<AlterarBeneficiarioResult>
{
    public int IdCotacao { get; set; }
    public IEnumerable<BeneficiarioInputModel>? Beneficiarios { get; set; }
}