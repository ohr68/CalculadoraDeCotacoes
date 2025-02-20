using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Coberturas.ExcluirCobertura;

public class ExcluirCoberturaCommandHandler(
    ApplicationDbContext context,
    IValidator<ExcluirCoberturaCommand> excluirCoberturaValidator)
    : IRequestHandler<ExcluirCoberturaCommand, ExcluirCoberturaResult>
{
    public async Task<ExcluirCoberturaResult> Handle(ExcluirCoberturaCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await excluirCoberturaValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = await context.Cotacoes
            .Include(c => c.Segurado)
            .SingleOrDefaultAsync(c => c.Id == request.IdCotacao, cancellationToken);

        if (cotacao is null)
            throw new NotFoundException($"Cotação de id {request.IdCotacao} não encontrada.");

        var cobertura =
            await context.Coberturas.SingleOrDefaultAsync(c => c.Id == request.IdCobertura, cancellationToken);

        if (cobertura is null)
            throw new NotFoundException($"Cobertura de id {request.IdCobertura} não encontrada.");

        var cotacaoCobertura = await context.CotacoesCoberturas.SingleOrDefaultAsync(
            cc => cc.IdCotacao == request.IdCotacao && cc.IdCobertura == request.IdCobertura, cancellationToken);

        if (cotacaoCobertura is null)
            throw new NotFoundException(
                $"Cobertura de id {request.IdCobertura} não encontrada na cotação de id {request.IdCotacao}.");

        context.CotacoesCoberturas.Remove(cotacaoCobertura);
        
        cotacao.Segurado!.CalcularValorPremio();
        context.Entry(cotacao).State = EntityState.Modified;
        
        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new ExcluirCoberturaResult(sucesso);
    }
}