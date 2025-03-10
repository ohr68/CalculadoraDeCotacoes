﻿using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Models.InputModels;

public class BeneficiarioInputModelValidator : AbstractValidator<BeneficiarioInputModel>
{
    public BeneficiarioInputModelValidator()
    {
        RuleFor(cb => cb.Nome)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do beneficiário.")
            .MaximumLength(150)
            .WithMessage("O tamanho máximo permitido para o nome do beneficiário é de 150 caracteres.");

        RuleFor(cb => cb.Percentual)
            .NotEmpty()
            .WithMessage("É obrigatório informar o percentual.");
        
        RuleFor(cb => cb.IdParentesco)
            .IsInEnum()
            .WithMessage("Informe um tipo de parentesco válido.");
    }
}
