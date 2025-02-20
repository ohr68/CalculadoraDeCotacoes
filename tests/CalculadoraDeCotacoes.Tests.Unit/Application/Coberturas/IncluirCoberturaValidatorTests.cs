using CalculadoraDeCotacoes.Application.Coberturas.IncluirCobertura;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Coberturas;

public class IncluirCoberturaValidatorTests
{
    private readonly IValidator<IncluirCoberturaCommand> _validator = new IncluirCoberturaValidator();
    
    [Fact(DisplayName = "Ao não informar o id da cotação deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_Empty()
    {
        // Arrange
        var command = new IncluirCoberturaCommand()
        {
            IdCotacao = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCotacao)
            .WithErrorMessage("É obrigatório informar o id da cotação.");
    }
    
    [Fact(DisplayName = "Ao informar um id menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_LessThan_One()
    {
        // Arrange
        var command = new IncluirCoberturaCommand()
        {
            IdCotacao = -1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCotacao)
            .WithErrorMessage("Informe um id de cotação válido.");
    }
    
    [Fact(DisplayName = "Ao não informar o id da cobertura deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cobertura_Is_Empty()
    {
        // Arrange
        var command = new IncluirCoberturaCommand()
        {
            IdCobertura = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCobertura)
            .WithErrorMessage("É obrigatório informar o id da cobertura.");
    }
    
    [Fact(DisplayName = "Ao informar um id de cobertura menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cobertura_Is_LessThan_One()
    {
        // Arrange
        var command = new IncluirCoberturaCommand()
        {
            IdCobertura = -1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCobertura)
            .WithErrorMessage("Informe um id de cobertura válido.");
    }
    
    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var command = new IncluirCoberturaCommand()
        {
            IdCotacao = 1,
            IdCobertura = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdCotacao);
        result.ShouldNotHaveValidationErrorFor(x => x.IdCobertura);
    }
}