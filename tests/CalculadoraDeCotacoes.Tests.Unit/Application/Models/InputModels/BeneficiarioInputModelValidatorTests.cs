using Bogus;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Enums;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Models.InputModels;

public class BeneficiarioInputModelValidatorTests
{
    private readonly IValidator<BeneficiarioInputModel> _validator = new BeneficiarioInputModelValidator();

    [Fact(DisplayName = "Ao não informar o nome deve retornar um erro")]
    public void Should_Have_Error_When_Nome_Is_Empty()
    {
        // Arrange
        var model = new BeneficiarioInputModel()
        {
            Nome = string.Empty,
            IdParentesco = TipoParentesco.Filho,
            Percentual = 100
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Nome)
            .WithErrorMessage("É obrigatório informar o nome do beneficiário.");
    }

    [Fact(DisplayName =
        "Ao informar um valor que ultrapassa o tamanho máximo permitido para o nome deve retornar um erro")]
    public void Should_Have_Error_When_Nome_Segurado_Length_Is_Above_Maximum_Allowed()
    {
        // Arrange
        var model = new BeneficiarioInputModel()
        {
            Nome = new Faker().Random.String2(151),
            IdParentesco = TipoParentesco.Filho,
            Percentual = 100
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Nome)
            .WithErrorMessage("O tamanho máximo permitido para o nome do beneficiário é de 150 caracteres.");
    }

    [Fact(DisplayName = "Ao não informar o percentual deve retornar um erro")]
    public void Should_Have_Error_When_Percentual_Is_Empty()
    {
        // Arrange
        var model = new BeneficiarioInputModel()
        {
            Nome = new Faker().Person.FullName,
            IdParentesco = TipoParentesco.Filho,
            Percentual = 0
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Percentual)
            .WithErrorMessage("É obrigatório informar o percentual.");
    }

    [Fact(DisplayName = "Ao informar um tipo de parentesco inválido deve retornar um erro")]
    public void Should_Have_Error_When_Id_Parentesco_Is_Invalid()
    {
        // Arrange
        var model = new BeneficiarioInputModel()
        {
            Nome = new Faker().Person.FullName,
            IdParentesco = (TipoParentesco)6,
            Percentual = 0
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.IdParentesco)
            .WithErrorMessage("Informe um tipo de parentesco válido.");
    }
    
    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var model = new BeneficiarioInputModel()
        {
            Nome = new Faker().Person.FullName,
            IdParentesco = TipoParentesco.Filho,
            Percentual = 100
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        result.ShouldNotHaveValidationErrorFor(x => x.IdParentesco);
        result.ShouldNotHaveValidationErrorFor(x => x.Percentual);
    }
}