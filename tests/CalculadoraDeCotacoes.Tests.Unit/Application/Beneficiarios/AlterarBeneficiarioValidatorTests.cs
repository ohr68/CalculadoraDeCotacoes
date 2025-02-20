using Bogus;
using CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Enums;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Beneficiarios;

public class AlterarBeneficiarioValidatorTests
{
    private readonly IValidator<AlterarBeneficiarioCommand> _validator = new AlterarBeneficiarioValidator();
    
    [Fact(DisplayName = "Ao não informar nenhum beneficiário deve retornar erro")]
    public void Should_Have_Error_When_Id_Produto_Is_Empty()
    {
        // Arrange
        var command = new AlterarBeneficiarioCommand()
        {
            Beneficiarios = null
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Beneficiarios)
            .WithErrorMessage("É obrigatório informar ao menos um beneficiário.");
    }
    
    [Fact(DisplayName = "Ao informar a porcentagem incorreta para os beneficiários deve retornar um erro")]
    public void Should_Have_Error_When_Beneficiarios_Doesnt_Reach_100()
    {
        // Arrange
        var command = new AlterarBeneficiarioCommand()
        {
            Beneficiarios = new List<BeneficiarioInputModel>()
            {
                new() { Nome = new Faker().Person.FullName, IdParentesco = TipoParentesco.Filho, Percentual = 10 }
            }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Beneficiarios)
            .WithErrorMessage("A soma do percentual deve ser 100.");
    }

}