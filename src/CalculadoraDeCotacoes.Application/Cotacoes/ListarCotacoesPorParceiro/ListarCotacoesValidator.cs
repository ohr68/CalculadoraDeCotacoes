using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;

public class ListarCotacoesValidator : AbstractValidator<ListarCotacoesPorParceiroQuery>
{
    public ListarCotacoesValidator()
    {
        RuleFor(l => l.IdParceiro)
            .NotEmpty()
            .WithMessage("É obrigatório informar o Id do parceiro.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Um id de parceiro válido deve ser informado.");
    }
}