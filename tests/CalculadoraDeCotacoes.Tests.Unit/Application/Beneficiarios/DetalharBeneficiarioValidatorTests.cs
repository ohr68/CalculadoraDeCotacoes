using CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Beneficiarios;

public class DetalharBeneficiarioValidatorTests
{
    private readonly IValidator<DetalharBeneficiarioQuery> _validator = new DetalharBeneficiarioValidator();
    
    [Fact(DisplayName = "Ao não informar o id da cotação deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_Empty()
    {
        // Arrange
        var query = new DetalharBeneficiarioQuery()
        {
            IdCotacao = 0
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCotacao)
            .WithErrorMessage("É obrigatório informar o id da cotação.");
    }
    
    [Fact(DisplayName = "Ao informar um id menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Id_Cotacao_Is_LessThan_One()
    {
        // Arrange
        var query = new DetalharBeneficiarioQuery()
        {
            IdCotacao = -1
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdCotacao)
            .WithErrorMessage("Informe um id de cotação válido.");
    }
    
    [Fact(DisplayName = "Ao não informar o id do beneficiário deve retornar um erro")]
    public void Should_Have_Error_When_Id_Beneficiario_Is_Empty()
    {
        // Arrange
        var query = new DetalharBeneficiarioQuery()
        {
            IdBeneficiario = 0
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdBeneficiario)
            .WithErrorMessage("É obrigatório informar o id do beneficiario.");
    }
    
    [Fact(DisplayName = "Ao informar um id de beneficiário menor que 1 deve retornar um erro")]
    public void Should_Have_Error_When_Id_Beneficiario_Is_LessThan_One()
    {
        // Arrange
        var query = new DetalharBeneficiarioQuery()
        {
            IdBeneficiario = -1
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(d => d.IdBeneficiario)
            .WithErrorMessage("Informe um ide de beneficiário válido.");
    }

    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {
        // Arrange
        var query = new DetalharBeneficiarioQuery()
        {
            IdCotacao = 1,
            IdBeneficiario = 1
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdCotacao);
        result.ShouldNotHaveValidationErrorFor(x => x.IdBeneficiario);
    }
}