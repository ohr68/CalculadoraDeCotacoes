using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;

public class DetalharBeneficiarioHandler(
    ApplicationDbContext context,
    IValidator<DetalharBeneficiarioQuery> detalharBeneficiarioValidator)
    : IRequestHandler<DetalharBeneficiarioQuery, DetalharBeneficiarioResult>
{
    public async Task<DetalharBeneficiarioResult> Handle(DetalharBeneficiarioQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await detalharBeneficiarioValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var beneficiario = await context.CotacoesBeneficiarios.SingleOrDefaultAsync(
            cb => cb.Id == request.IdBeneficiario && cb.IdCotacao == request.IdCotacao, cancellationToken);

        if (beneficiario is null)
            throw new NotFoundException(
                $"Nenhum beneficiário de id {request.IdBeneficiario} foi encontrado na cotação de id {request.IdCotacao}.");

        return beneficiario.Adapt<DetalharBeneficiarioResult>();
    }
}