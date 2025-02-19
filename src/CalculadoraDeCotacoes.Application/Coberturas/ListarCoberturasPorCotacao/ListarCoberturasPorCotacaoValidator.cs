﻿using CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;
using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;

public class ListarCoberturasPorCotacaoValidator : AbstractValidator<ListarBeneficiariosPorCotacaoQuery>
{
    public ListarCoberturasPorCotacaoValidator()
    {
        RuleFor(l => l.IdCotacao)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id da cotação.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Informe um id de cotação válido.");
    }
}