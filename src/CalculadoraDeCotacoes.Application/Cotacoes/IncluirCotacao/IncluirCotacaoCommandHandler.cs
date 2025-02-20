using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Common.Interfaces;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;

public class IncluirCotacaoCommandHandler(
    ApplicationDbContext context,
    IValidator<IncluirCotacaoCommand> incluirCotacaoValidator,
    IValidator<BeneficiarioInputModel> beneficiarioValidator,
    IValidator<CoberturaInputModel> coberturaValidator,
    IFaixaDeIdadeHelper faixaDeIdadeHelper)
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
                var beneficiarioValidationResult =
                    await beneficiarioValidator.ValidateAsync(beneficiario, cancellationToken);

                if (!beneficiarioValidationResult.IsValid)
                    throw new ValidationException(beneficiarioValidationResult.Errors);
            }
        }

        if (request.Coberturas is not null && request.Coberturas.Count > 0)
        {
            foreach (var cobertura in request.Coberturas)
            {
                var coberturaValidationResult =
                    await coberturaValidator.ValidateAsync(cobertura, cancellationToken);

                if (!coberturaValidationResult.IsValid)
                    throw new ValidationException(coberturaValidationResult.Errors);
            }
        }

        var faixaDeIdadeDoSegurado = await faixaDeIdadeHelper.ObterFaixaDeIdadeDoSegurado(request.DataNascimento, cancellationToken);

        var cotacao = request.Adapt<Cotacao>();

        foreach (var coberturaCotacao in cotacao.CotacoesCoberturas!)
        {
            if ((Domain.Enums.TipoCobertura)coberturaCotacao.Cobertura!.TipoCoberturaId !=
                Domain.Enums.TipoCobertura.Basica) continue;

            var cobertura = await context.Coberturas.SingleOrDefaultAsync(c => c.Id == coberturaCotacao.IdCobertura,
                                cancellationToken) ??
                            throw new NotFoundException(
                                $"Cobertura de id {coberturaCotacao.IdCobertura} não encontrada.");


            coberturaCotacao.ValorDesconto = faixaDeIdadeDoSegurado.Desconto > 0
                ? cobertura!.Valor * faixaDeIdadeDoSegurado.Desconto
                : 0;

            coberturaCotacao.ValorAgravo = faixaDeIdadeDoSegurado.Agravo > 0
                ? cobertura!.Valor * faixaDeIdadeDoSegurado.Agravo
                : 0;

            coberturaCotacao.ValorTotal =
                cobertura!.Valor - coberturaCotacao.ValorDesconto + coberturaCotacao.ValorAgravo;
        }

        cotacao.Segurado!.VerificarValorImportanciaSegurada();

        cotacao.Segurado!.CalcularValorPremio();

        await context.Cotacoes.AddAsync(cotacao, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);

        return cotacao.Adapt<IncluirCotacaoResult>();
    }
}