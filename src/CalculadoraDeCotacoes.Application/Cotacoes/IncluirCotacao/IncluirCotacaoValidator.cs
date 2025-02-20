using CalculadoraDeCotacoes.Domain.Enums;
using FluentValidation;

namespace CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;

public class IncluirCotacaoValidator : AbstractValidator<IncluirCotacaoCommand>
{
    public IncluirCotacaoValidator()
    {
        RuleFor(i => i.IdProduto)
            .NotEmpty()
            .WithMessage("É obrigatório informar o produto.");

        RuleFor(i => i.NomeSegurado)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do segurado.")
            .MaximumLength(150)
            .WithMessage("O tamanho máximo permitido para o nome do segurado é de 150 caracteres.");

        When(i => i.Ddd > 0, () =>
        {
            RuleFor(i => i.Telefone)
                .NotEmpty()
                .WithMessage("É obrigatório informar o telefone.");
        });

        When(i => i.Telefone > 0, () =>
        {
            RuleFor(i => i.Ddd)
                .NotEmpty()
                .WithMessage("É obrigatório informar o DDD.");
        });

        RuleFor(i => i.Endereco)
            .NotEmpty()
            .WithMessage("É obrigatório informar o endereço.")
            .MaximumLength(300)
            .WithMessage("O tamanho máximo permitido para o endereço é de 300 caracteres.");

        RuleFor(i => i.Cep)
            .NotEmpty()
            .WithMessage("É obrigatório informar o CEP.")
            .MaximumLength(8)
            .WithMessage("O tamanho máximo permitido para o CEP é de 8 caracteres.");

        RuleFor(i => i.Documento)
            .NotEmpty()
            .WithMessage("É obrigatório informar o documento.")
            .MaximumLength(30)
            .WithMessage("O tamanho máximo permitido para o documento é de 30 caracteres.");

        RuleFor(i => i.DataNascimento)
            .NotEmpty()
            .WithMessage("É obrigatório informar a data de nascimento.");

        RuleFor(i => i.Premio)
            .NotEmpty()
            .WithMessage("É obrigatório informar o prêmio.");

        RuleFor(i => i.ImportanciaSegurada)
            .NotEmpty()
            .WithMessage("É obrigatório informar a importância segurada.");
        
        When(i => i.Beneficiarios is not null && i.Beneficiarios.Count > 0, () =>
        {
            RuleFor(b => b.Beneficiarios).Custom((list, context) =>
            {
                if (list!.Sum(b => b.Percentual) != 100)
                    context.AddFailure("A soma do percentual deve ser 100.");
            });
        });

        RuleFor(i => i.Coberturas)
            .NotEmpty()
            .WithMessage("É obrigatório informar ao menos duas coberturas.");

        When(i => i.Coberturas is not null && i.Coberturas.Count > 0, () =>
        {
            RuleFor(b => b.Coberturas).Custom((list, context) =>
            {
                if (list!.Count(c => c.TipoCobertura == TipoCobertura.Basica) != 1)
                    context.AddFailure("É obrigatório informar apenas uma cobertura do tipo 'Básica'.");

                if (list!.All(c => c.TipoCobertura != TipoCobertura.Adicional))
                    context.AddFailure("É obrigatório informar ao menos uma cobertura do tipo 'Adicional'.");
            });
        });
    }
}