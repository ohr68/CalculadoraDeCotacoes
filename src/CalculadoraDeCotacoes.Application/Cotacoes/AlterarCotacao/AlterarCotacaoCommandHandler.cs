using CalculadoraDeCotacoes.Application.Common.Interfaces;
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
    IAuthService authService)
    : IRequestHandler<AlterarCotacaoCommand, AlterarCotacaoResult>
{
    public async Task<AlterarCotacaoResult> Handle(AlterarCotacaoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await alterarCotacaoValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = request.Adapt<Cotacao>();
        
        var idParceiro = await authService.ObterIdParceiro(cancellationToken);
        cotacao.IdParceiro = idParceiro;

        context.Entry(cotacao).State = EntityState.Modified;

        var rowsAffected = await context.SaveChangesAsync(cancellationToken);

        return new AlterarCotacaoResult(rowsAffected > 0);
    }
}