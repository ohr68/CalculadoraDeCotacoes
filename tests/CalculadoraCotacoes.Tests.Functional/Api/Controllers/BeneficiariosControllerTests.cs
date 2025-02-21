using System.Text.Json;
using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;
using CalculadoraDeCotacoes.Application.Common.Constants;
using FluentAssertions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CalculadoraCotacoes.Tests.Functional.Api.Controllers;

public class BeneficiariosControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BeneficiariosControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add(Auth.Secret, "XPTO2");
    }

    [Fact(DisplayName = "Listar beneficiários com sucesso.")]
    public async Task ListarBeneficiarios_ComSucesso()
    {
        // Arrange
        var query = new ListarBeneficiariosPorCotacaoQuery()
        {
            IdCotacao = 6,
            Pagina = 1,
            RegistrosPorPagina = 10
        };

        // Act
        var response =
            await _client.GetAsync(
                $"/api/beneficiarios?idCotacao={query.IdCotacao}&pagina={query.Pagina}&registrosPorPagina={query.RegistrosPorPagina}");

        // Assert
        response.EnsureSuccessStatusCode(); // Fails the test if not 2xx
        response.Content.Headers.ContentType!.ToString().Should().Contain("application/json");

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var result = JsonSerializer.Deserialize<PaginatedListResponse<ListarBeneficiariosPorCotacaoResult>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }
}