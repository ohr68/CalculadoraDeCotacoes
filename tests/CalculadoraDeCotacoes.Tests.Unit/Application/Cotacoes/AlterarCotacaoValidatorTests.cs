using Bogus;
using Bogus.Extensions.Brazil;
using CalculadoraDeCotacoes.Application.Cotacoes.AlterarCotacao;
using FluentValidation;
using FluentValidation.TestHelper;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Cotacoes;

public class AlterarCotacaoValidatorTests
{
     private readonly IValidator<AlterarCotacaoCommand> _validator = new AlterarCotacaoValidator();

    [Fact(DisplayName = "Ao não informar o id do produto deve retornar um erro")]
    public void Should_Have_Error_When_Id_Produto_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 0,
            NomeSegurado = string.Empty,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.IdProduto)
            .WithErrorMessage("É obrigatório informar o produto.");
    }
    
    [Fact(DisplayName = "Ao não informar o nome do segurado deve retornar um erro")]
    public void Should_Have_Error_When_Nome_Segurado_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = string.Empty,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.NomeSegurado)
            .WithErrorMessage("É obrigatório informar o nome do segurado.");
    }

    [Fact(DisplayName =
        "Ao informar um valor que ultrapassa o tamanho máximo permitido para o nome do segurado deve retornar um erro")]
    public void Should_Have_Error_When_Nome_Segurado_Length_Is_Above_Maximum_Allowed()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Random.String2(151),
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.NomeSegurado)
            .WithErrorMessage("O tamanho máximo permitido para o nome do segurado é de 150 caracteres.");
    }

    [Fact(DisplayName = "Ao informar o DDD e não informar o telefone deve retornar um erro")]
    public void Should_Have_Error_When_Ddd_Is_Not_Empty_And_Telefone_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 0,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Telefone)
            .WithErrorMessage("É obrigatório informar o telefone.");
    }

    [Fact(DisplayName = "Ao informar o telefone e não informar o DDD deve retornar um erro")]
    public void Should_Have_Error_When_Telefone_Is_Not_Empty_And_Ddd_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 0,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Ddd)
            .WithErrorMessage("É obrigatório informar o DDD.");
    }

    [Fact(DisplayName = "Ao não informar o endereço deve retornar um erro")]
    public void Should_Have_Error_When_Endereco_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = string.Empty,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Endereco)
            .WithErrorMessage("É obrigatório informar o endereço.");
    }

    [Fact(DisplayName =
        "Ao informar um valor que ultrapassa o tamanho máximo permitido para o endereço deve retornar um erro")]
    public void Should_Have_Error_When_Endereco_Length_Is_Above_Maximum_Allowed()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Random.String2(301),
            Cep = new Faker().Person.Address.ZipCode,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Endereco)
            .WithErrorMessage("O tamanho máximo permitido para o endereço é de 300 caracteres.");
    }

    [Fact(DisplayName = "Ao não informar o CEP deve retornar um erro")]
    public void Should_Have_Error_When_Cep_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = string.Empty,
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Cep)
            .WithErrorMessage("É obrigatório informar o CEP.");
    }

    [Fact(DisplayName =
        "Ao informar um valor que ultrapassa o tamanho máximo permitido para o CEP deve retornar um erro")]
    public void Should_Have_Error_When_Cep_Length_Is_Above_Maximum_Allowed()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Random.String2(151),
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Random.String2(9),
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Cep)
            .WithErrorMessage("O tamanho máximo permitido para o CEP é de 8 caracteres.");
    }

    [Fact(DisplayName = "Ao não informar o documento deve retornar um erro")]
    public void Should_Have_Error_When_Documento_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = string.Empty,
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Documento)
            .WithErrorMessage("É obrigatório informar o documento.");
    }

    [Fact(DisplayName =
        "Ao informar um valor que ultrapassa o tamanho máximo permitido para o documento deve retornar um erro")]
    public void Should_Have_Error_When_Documento_Length_Is_Above_Maximum_Allowed()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Random.String2(151),
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Random.String2(9),
            Documento = new Faker().Random.String2(31),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.Documento)
            .WithErrorMessage("O tamanho máximo permitido para o documento é de 30 caracteres.");
    }

    [Fact(DisplayName = "Ao não informar a data de nascimento deve retornar um erro")]
    public void Should_Have_Error_When_Data_Nascimento_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = string.Empty,
            DataNascimento = DateOnly.MinValue,
            ImportanciaSegurada = 10_000
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.DataNascimento)
            .WithErrorMessage("É obrigatório informar a data de nascimento.");
    }

    [Fact(DisplayName = "Ao não informar a IS deve retornar um erro")]
    public void Should_Have_Error_When_Importancia_Segurada_Is_Empty()
    {
        // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = new Faker().Person.Address.ZipCode,
            Documento = string.Empty,
            DataNascimento = DateOnly.MinValue,
            ImportanciaSegurada = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.ImportanciaSegurada)
            .WithErrorMessage("É obrigatório informar a importância segurada.");
    }
    

    [Fact(DisplayName = "Ao informar dados válidos não deve retornar nenhum erro")]
    public void Should_Not_Have_Error_When_Valid_Model()
    {   // Arrange
        var command = new AlterarCotacaoCommand()
        {
            IdProduto = 1,
            NomeSegurado = new Faker().Person.FullName,
            Ddd = 17,
            Telefone = 991919191,
            Endereco = new Faker().Person.Address.Street,
            Cep = "15783970",
            Documento = new Faker().Person.Cpf(),
            DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
                new Faker().Person.DateOfBirth.Day),
            ImportanciaSegurada = 10_000M
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.NomeSegurado);
        result.ShouldNotHaveValidationErrorFor(x => x.Ddd);
        result.ShouldNotHaveValidationErrorFor(x => x.Telefone);
        result.ShouldNotHaveValidationErrorFor(x => x.Endereco);
        result.ShouldNotHaveValidationErrorFor(x => x.Cep);
        result.ShouldNotHaveValidationErrorFor(x => x.Documento);
        result.ShouldNotHaveValidationErrorFor(x => x.DataNascimento);
        result.ShouldNotHaveValidationErrorFor(x => x.ImportanciaSegurada);
    }
}