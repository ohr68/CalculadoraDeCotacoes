using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;

public class IncluirCotacaoCommandHandler(
    ApplicationDbContext context,
    IValidator<IncluirCotacaoCommand> incluirCotacaoValidator,
    IValidator<BeneficiarioInputModel> beneficiarioValidator)
    : IRequestHandler<IncluirCotacaoCommand, IncluirCotacaoResult>
{
    public async Task<IncluirCotacaoResult> Handle(IncluirCotacaoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await incluirCotacaoValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (request.Beneficiarios is not null && request.Beneficiarios.Count > 0)
        {
            foreach (var beneficiario in request.Beneficiarios)
            {
                var beneficiariosValidationResult =
                    await beneficiarioValidator.ValidateAsync(beneficiario, cancellationToken);

                if (!beneficiariosValidationResult.IsValid)
                    throw new ValidationException(beneficiariosValidationResult.Errors);
            }
        }

        var cotacao = request.Adapt<Domain.Entities.Cotacao>();

        await context.Cotacoes.AddAsync(cotacao, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);

        return cotacao.Adapt<IncluirCotacaoResult>();
    }
}