using MediatR;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;

public class DetalharBeneficiarioQuery : IRequest<DetalharBeneficiarioResult>
{
    public int IdBeneficiario { get; set; }
    public int IdCotacao { get; set; }
}