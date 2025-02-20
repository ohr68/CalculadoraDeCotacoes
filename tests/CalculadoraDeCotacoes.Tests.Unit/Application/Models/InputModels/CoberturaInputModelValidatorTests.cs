using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Enums;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Models.InputModels;

public class CoberturaInputModelValidatorTests
{
    private readonly IValidator<CoberturaInputModel> _validator = new CoberturaInputModelValidator();
    
    [Fact(DisplayName = "Ao não informar o id da cobertura deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cobertura_Is_Empty()
    {
        // Arrange
        var model = new CoberturaInputModel
        {
            IdCobertura = 0,
            TipoCobertura = TipoCobertura.Basica
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCobertura)
            .WithErrorMessage("É obrigatório informar o id da cobertura.");
    }
    
    [Fact(DisplayName = "Ao informar um id menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_LessThan_One()
    {
        var model = new CoberturaInputModel
        {
            IdCobertura = 0,
            TipoCobertura = TipoCobertura.Basica
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCobertura)
            .WithErrorMessage("Informe um id de cobertura válido.");
    }

    [Fact(DisplayName = "Ao informar um tipo de cobertura inválido deve retornar um erro")]
    public void Should_Have_Error_When_Id_Tipo_Cobertura_Is_Invalid()
    {
        var model = new CoberturaInputModel
        {
            IdCobertura = 1,
            TipoCobertura = (TipoCobertura)3
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.TipoCobertura)
            .WithErrorMessage("Informe um tipo de cobertura válido.");
    }
    
    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var model = new CoberturaInputModel
        {
            IdCobertura = 1,
            TipoCobertura = TipoCobertura.Basica
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdCobertura);
        result.ShouldNotHaveValidationErrorFor(x => x.TipoCobertura);
    }
}