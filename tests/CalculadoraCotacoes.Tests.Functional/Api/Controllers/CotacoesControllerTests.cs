using System.Net;
using System.Net.Http.Json;
using Bogus;
using Bogus.Extensions.Brazil;
using CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Enums;
using FluentAssertions;

namespace CalculadoraCotacoes.Tests.Functional.Api.Controllers;

// public class CotacoesControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
// {
//     private readonly HttpClient _client = factory.CreateClient();
//
//     [Fact(DisplayName = "Cadastrar contato com sucesso.")]
//     [Trait("", value: "Teste Integração")]
//     public async Task CadastrarCotacao_ComSucesso()
//     {
//         // // Arrange
//         // var command = new IncluirCotacaoCommand()
//         // {
//         //     IdProduto = 1,
//         //     NomeSegurado = new Faker().Person.FullName,
//         //     Ddd = 17,
//         //     Telefone = 991919191,
//         //     Endereco = new Faker().Person.Address.Street,
//         //     Cep = "15783970",
//         //     Documento = new Faker().Person.Cpf(),
//         //     DataNascimento = new DateOnly(new Faker().Person.DateOfBirth.Year, new Faker().Person.DateOfBirth.Month,
//         //         new Faker().Person.DateOfBirth.Day),
//         //     ImportanciaSegurada = 10_000M,
//         //     Beneficiarios = new List<BeneficiarioInputModel>()
//         //     {
//         //         new() { Nome = new Faker().Person.FullName, IdParentesco = TipoParentesco.Filho, Percentual = 100 }
//         //     },
//         //     Coberturas = new List<CoberturaInputModel>()
//         //     {
//         //         new() { IdCobertura = 1, TipoCobertura = TipoCobertura.Basica },
//         //         new() { IdCobertura = 2, TipoCobertura = TipoCobertura.Adicional },
//         //     }
//         // };
//         //
//         // // Act
//         // var response = await _client.PostAsJsonAsync("/api/cotacoes", command);
//         //
//         // // Assert
//         // response.StatusCode.Should().Be(HttpStatusCode.Created);
//     }
// }