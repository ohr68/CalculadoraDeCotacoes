using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;

public class DetalharCotacaoHandler(
    ApplicationDbContext context,
    IValidator<DetalharCotacaoQuery> detalharCotacaoValidator)
    : IRequestHandler<DetalharCotacaoQuery, DetalharCotacaoResult>
{
    public async Task<DetalharCotacaoResult> Handle(DetalharCotacaoQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await detalharCotacaoValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = await context.Cotacoes.SingleOrDefaultAsync(c => c.Id == request.IdCotacao, cancellationToken);

        if (cotacao is null)
            throw new NotFoundException($"Cotação de id {request.IdCotacao} não encontrada.");

        return cotacao.Adapt<DetalharCotacaoResult>();
    }
}