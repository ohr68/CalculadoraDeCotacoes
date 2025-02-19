using MediatR;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ExcluirBeneficiario;

public class ExcluirBeneficiarioCommand : IRequest<ExcluirBeneficiarioResult>
{
    public int IdCotacao { get; set; }
    public int IdBeneficiario { get; set; }
}