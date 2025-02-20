using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;

public class DetalharCotacaoValidator : AbstractValidator<DetalharCotacaoQuery>
{
    public DetalharCotacaoValidator()
    {
        RuleFor(d => d.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");
    }
}