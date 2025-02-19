﻿using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Coberturas.ExcluirCobertura;

public class ExcluirCoberturaValidator : AbstractValidator<ExcluirCoberturaCommand>
{
    public ExcluirCoberturaValidator()
    {
        RuleFor(l => l.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");
        
        RuleFor(l => l.IdCobertura)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cobertura.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cobertura válido.");
    }
}