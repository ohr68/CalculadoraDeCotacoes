using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;

public class AlterarBeneficiarioValidator : AbstractValidator<AlterarBeneficiarioCommand>
{
    public AlterarBeneficiarioValidator()
    {
        RuleFor(b => b.Beneficiarios)
            .NotEmpty()
            .WithMessage("É obrigatório informar ao menos um beneficiário.");
        
        When(i => i.Beneficiarios is not null && i.Beneficiarios.Any(), () =>
        {
            RuleFor(b => b.Beneficiarios).Custom((list, context) =>
            {
                if (list!.Sum(b => b.Percentual) != 100)
                    context.AddFailure("A soma do percentual deve ser 100.");
            });
        });
    }
}