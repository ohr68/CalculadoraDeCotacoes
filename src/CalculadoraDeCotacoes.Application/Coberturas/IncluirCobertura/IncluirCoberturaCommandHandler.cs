using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Coberturas.IncluirCobertura;

public class IncluirCoberturaCommandHandler(
    ApplicationDbContext context,
    IValidator<IncluirCoberturaCommand> incluirCoberturaValidator)
    : IRequestHandler<IncluirCoberturaCommand, IncluirCoberturaResult>
{
    public async Task<IncluirCoberturaResult> Handle(IncluirCoberturaCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await incluirCoberturaValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = await context.Cotacoes.SingleOrDefaultAsync(c => c.Id == request.IdCotacao, cancellationToken);

        if (cotacao is null)
            throw new NotFoundException($"Cotação de id {request.IdCotacao} não encontrada.");

        var cobertura =
            await context.Coberturas.SingleOrDefaultAsync(c => c.Id == request.IdCobertura, cancellationToken);

        if (cobertura is null)
            throw new NotFoundException($"Cobertura de id {request.IdCobertura} não encontrada.");

        context.CotacoesCoberturas.Add(request.Adapt<CotacaoCobertura>());
        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new IncluirCoberturaResult(sucesso);
    }
}