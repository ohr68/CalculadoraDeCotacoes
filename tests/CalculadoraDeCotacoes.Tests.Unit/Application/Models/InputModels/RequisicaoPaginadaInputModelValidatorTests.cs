using CalculadoraDeCotacoes.Application.Models.InputModels;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Models.InputModels;

public class RequisicaoPaginadaInputModelValidatorTests
{
    private readonly IValidator<RequisicaoPaginadaInputModel> _validator = new RequisicaoPaginadaInputModelValidator();
    
    [Fact(DisplayName = "Ao não informar o número da página deve retornar um erro")]
    public void Should_Have_Error_When_Pagina_Is_Empty()
    {
        // Arrange
        var model = new RequisicaoPaginadaInputModel()
        {
            Pagina = 0
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.Pagina)
            .WithErrorMessage("É obrigatório informar a página.");
    }
    
    [Fact(DisplayName = "Ao informar um o número de página menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Pagina_Is_LessThan_One()
    {
        // Arrange
        var model = new RequisicaoPaginadaInputModel()
        {
            Pagina = -1
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.Pagina)
            .WithErrorMessage("Uma página válida deve ser informada.");
    }

    [Fact(DisplayName = "Ao não informar a quantidade de registros por página deve retornar um erro")]
    public void Should_Have_Error_When_Registros_Por_Pagina_Is_Empty()
    {
        // Arrange
        var model = new RequisicaoPaginadaInputModel()
        {
            RegistrosPorPagina = 0
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.RegistrosPorPagina)
            .WithErrorMessage("É obrigatório informar a quantidade de registros por página.");
    }
    
    [Fact(DisplayName = "Ao informar a quantidade de registros por página menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Registros_Por_Pagina_Is_LessThan_One()
    {
        // Arrange
        var model = new RequisicaoPaginadaInputModel()
        {
            RegistrosPorPagina = -1
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.RegistrosPorPagina)
            .WithErrorMessage("Um valor válido deve ser informado para a quantidade de registros por página.");
    }
    
    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var model = new RequisicaoPaginadaInputModel()
        {
            Pagina = 1,
            RegistrosPorPagina = 10
        };

        // Act
        var result = _validator.TestValidate(model);


        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Pagina);
        result.ShouldNotHaveValidationErrorFor(x => x.RegistrosPorPagina);
    }
}