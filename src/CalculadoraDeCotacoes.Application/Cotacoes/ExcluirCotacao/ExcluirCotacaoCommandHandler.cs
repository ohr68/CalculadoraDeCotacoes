using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;

public class ExcluirCotacaoCommandHandler(
    ApplicationDbContext context,
    IValidator<ExcluirCotacaoCommand> excluirCotacaoValidator)
    : IRequestHandler<ExcluirCotacaoCommand, ExcluirCotacaoResult>
{
    public async Task<ExcluirCotacaoResult> Handle(ExcluirCotacaoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await excluirCotacaoValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = await context.Cotacoes.SingleOrDefaultAsync(c => c.Id == request.IdCotacao, cancellationToken);

        if (cotacao is null)
            throw new NotFoundException($"Cotação de id {request.IdCotacao} não encontrada.");

        context.Cotacoes.Remove(cotacao);
        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new ExcluirCotacaoResult(sucesso);
    }
}