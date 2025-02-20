using CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Cotacoes;

public class ExcluirCotacaoValidatorTests
{
    private readonly IValidator<ExcluirCotacaoCommand> _validator = new ExcluirCotacaoValidator();
    
    [Fact(DisplayName = "Ao não informar o id da cotação deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_Empty()
    {
        // Arrange
        var command = new ExcluirCotacaoCommand()
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
        var command = new ExcluirCotacaoCommand()
        {
            IdCotacao = -1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCotacao)
            .WithErrorMessage("Informe um id de cotação válido.");
    }
    
    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var command = new ExcluirCotacaoCommand()
        {
            IdCotacao = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdCotacao);
    }
}