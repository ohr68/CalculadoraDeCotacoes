﻿using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Bogus.Extensions.Brazil;
using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Common.Constants;
using CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;
using CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Enums;
using FluentAssertions;

namespace CalculadoraCotacoes.Tests.Functional.Api.Controllers;

public class CotacoesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CotacoesControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add(Auth.Secret, "XPTO2");
    }

    [Fact(DisplayName = "Cadastrar cotação com sucesso.")]
    public async Task CadastrarCotacao_ComSucesso()
    {
        // Arrange
        var command = new IncluirCotacaoCommand()
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
            ImportanciaSegurada = 1_000M,
            Beneficiarios = new List<BeneficiarioInputModel>()
            {
                new() { Nome = new Faker().Person.FullName, IdParentesco = TipoParentesco.Filho, Percentual = 100 }
            },
            Coberturas = new List<CoberturaInputModel>()
            {
                new() { IdCobertura = 1, TipoCobertura = TipoCobertura.Basica },
                new() { IdCobertura = 2, TipoCobertura = TipoCobertura.Adicional },
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/cotacoes", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact(DisplayName = "Listar cotações com sucesso.")]
    public async Task ListarCotacoes_ComSucesso()
    {
        // Arrange
        var query = new ListarCotacoesPorParceiroQuery()
        {
            Pagina = 1,
            RegistrosPorPagina = 10
        };

        // Act
        var response =
            await _client.GetAsync(
                $"/api/cotacoes?pagina={query.Pagina}&registrosPorPagina={query.RegistrosPorPagina}");

        // Assert
        response.EnsureSuccessStatusCode(); // Fails the test if not 2xx
        response.Content.Headers.ContentType!.ToString().Should().Contain("application/json");

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var result = JsonSerializer.Deserialize<PaginatedListResponse<ListarCotacoesPorParceiroResult>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }
}