﻿using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ExcluirBeneficiario;

public class ExcluirBeneficiarioValidator : AbstractValidator<ExcluirBeneficiarioCommand>
{
    public ExcluirBeneficiarioValidator()
    {
        RuleFor(d => d.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");

        RuleFor(d => d.IdBeneficiario)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id do beneficiario.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um ide de beneficiário válido.");
    }
}