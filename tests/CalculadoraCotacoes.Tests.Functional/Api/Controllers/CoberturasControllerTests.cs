using System.Text.Json;
using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;
using CalculadoraDeCotacoes.Application.Common.Constants;
using FluentAssertions;

namespace CalculadoraCotacoes.Tests.Functional.Api.Controllers;

public class CoberturasControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CoberturasControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add(Auth.Secret, "XPTO2");
    }

    [Fact(DisplayName = "Listar coberturas com sucesso.")]
    public async Task ListarCoberturas_ComSucesso()
    {
        // Arrange
        var query = new ListarCoberturasPorCotacaoQuery()
        {
            IdCotacao = 6,
            Pagina = 1,
            RegistrosPorPagina = 10
        };

        // Act
        var response =
            await _client.GetAsync(
                $"/api/coberturas?idCotacao={query.IdCotacao}&pagina={query.Pagina}&registrosPorPagina={query.RegistrosPorPagina}");

        // Assert
        response.EnsureSuccessStatusCode(); // Fails the test if not 2xx
        response.Content.Headers.ContentType!.ToString().Should().Contain("application/json");

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var result = JsonSerializer.Deserialize<PaginatedListResponse<ListarCoberturasPorCotacaoResult>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }
}