using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;

public class ExcluirCotacaoValidator : AbstractValidator<ExcluirCotacaoCommand>
{
    public ExcluirCotacaoValidator()
    {
        RuleFor(e => e.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");
    }
}