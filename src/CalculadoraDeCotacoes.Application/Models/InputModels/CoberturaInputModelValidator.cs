using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Models.InputModels;

public class CoberturaInputModelValidator : AbstractValidator<CoberturaInputModel>
{
    public CoberturaInputModelValidator()
    {
        RuleFor(c => c.IdCobertura)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cobertura.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cobertura válido.");

        RuleFor(c => c.TipoCobertura)
            .IsInEnum()
            .WithMessage("Informe um tipo de cobertura válido.");
    }
}