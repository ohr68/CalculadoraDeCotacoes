using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;

public class ListarBeneficiariosPorCotacaoValidator : AbstractValidator<ListarBeneficiariosPorCotacaoQuery>
{
    public ListarBeneficiariosPorCotacaoValidator()
    {
        RuleFor(l => l.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");
    }
}