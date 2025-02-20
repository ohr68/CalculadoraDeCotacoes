using CalculadoraDeCotacoes.Application.Common.Interfaces;
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
    IValidator<IncluirCoberturaCommand> incluirCoberturaValidator,
    IFaixaDeIdadeHelper faixaDeIdadeHelper)
    : IRequestHandler<IncluirCoberturaCommand, IncluirCoberturaResult>
{
    public async Task<IncluirCoberturaResult> Handle(IncluirCoberturaCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await incluirCoberturaValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cotacao = await context.Cotacoes
            .Include(c => c.CotacoesCoberturas)
            .Include(c => c.Segurado)
            .SingleOrDefaultAsync(c => c.Id == request.IdCotacao, cancellationToken);

        if (cotacao is null)
            throw new NotFoundException($"Cotação de id {request.IdCotacao} não encontrada.");

        var cobertura =
            await context.Coberturas.SingleOrDefaultAsync(c => c.Id == request.IdCobertura, cancellationToken);

        if (cobertura is null)
            throw new NotFoundException($"Cobertura de id {request.IdCobertura} não encontrada.");

        var faixaDeIdadeDoSegurado =
            await faixaDeIdadeHelper.ObterFaixaDeIdadeDoSegurado(cotacao.Segurado!.DataNascimento, cancellationToken);

        var coberturaCotacao = request.Adapt<CotacaoCobertura>();

        if ((Domain.Enums.TipoCobertura)coberturaCotacao.Cobertura!.TipoCoberturaId ==
            Domain.Enums.TipoCobertura.Basica)
        {
            coberturaCotacao.ValorDesconto = faixaDeIdadeDoSegurado.Desconto > 0
                ? cobertura!.Valor * faixaDeIdadeDoSegurado.Desconto
                : 0;

            coberturaCotacao.ValorAgravo = faixaDeIdadeDoSegurado.Agravo > 0
                ? cobertura!.Valor * faixaDeIdadeDoSegurado.Agravo
                : 0;

            coberturaCotacao.ValorTotal =
                cobertura!.Valor - coberturaCotacao.ValorDesconto + coberturaCotacao.ValorAgravo;
        }

        cotacao.Segurado.CalcularValorPremio();

        context.Entry(cotacao).State = EntityState.Modified;

        context.CotacoesCoberturas.Add(coberturaCotacao);
        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new IncluirCoberturaResult(sucesso);
    }
}