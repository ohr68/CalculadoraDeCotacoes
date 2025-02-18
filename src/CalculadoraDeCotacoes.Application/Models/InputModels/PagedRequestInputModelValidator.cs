using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Models.InputModels;

public class PagedRequestInputModelValidator : AbstractValidator<RequisicaoPaginadaInputModel>
{
    public PagedRequestInputModelValidator()
    {
        RuleFor(p => p.Pagina)
            .NotEmpty()
            .WithMessage("É obrigatório informar a página.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Uma página válida deve ser informada.");

        RuleFor(p => p.RegistrosPorPagina)
            .NotEmpty()
            .WithMessage("É obrigatório informar a quantidade de registros por página.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Um valor válido deve ser informado para a quantidade de registros por página.");
    }
}