using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ExcluirBeneficiario;

public class ExcluirBeneficiarioCommandHandler(
    ApplicationDbContext context,
    IValidator<ExcluirBeneficiarioCommand> excluirBeneficiarioValidator)
    : IRequestHandler<ExcluirBeneficiarioCommand, ExcluirBeneficiarioResult>
{
    public async Task<ExcluirBeneficiarioResult> Handle(ExcluirBeneficiarioCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await excluirBeneficiarioValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var beneficiario = await context.CotacoesBeneficiarios.SingleOrDefaultAsync(
            cb => cb.Id == request.IdBeneficiario && cb.IdCotacao == request.IdCotacao, cancellationToken);

        if (beneficiario is null)
            throw new NotFoundException(
                $"Nenhum beneficiário de id {request.IdBeneficiario} foi encontrado na cotação de id {request.IdCotacao}.");

        context.Remove(beneficiario);

        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new ExcluirBeneficiarioResult(sucesso);
    }
}