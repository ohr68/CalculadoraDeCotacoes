using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Cotacoes.AlterarCotacao;

public class AlterarCotacaoCommandHandler(
    ApplicationDbContext context,
    IValidator<AlterarCotacaoCommand> alterarCotacaoValidator,
    IValidator<BeneficiarioInputModel> beneficiarioValidator)
    : IRequestHandler<AlterarCotacaoCommand, AlterarCotacaoResult>
{
    public async Task<AlterarCotacaoResult> Handle(AlterarCotacaoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await alterarCotacaoValidator.ValidateAsync(request, cancellationToken);

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

        var cotacao = request.Adapt<Cotacao>();

        context.Entry(cotacao).State = EntityState.Modified;

        var rowsAffected = await context.SaveChangesAsync(cancellationToken);

        return new AlterarCotacaoResult(rowsAffected > 0);
    }
}