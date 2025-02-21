using CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;
using CalculadoraDeCotacoes.Application.Common.Constants;
using FluentAssertions;

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
    [Trait("", value: "Teste Integração")]
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
        var response = await _client.GetAsync($"/api/beneficiarios?idCotacao={query.IdCotacao}&pagina={query.Pagina}&registrosPorPagina={query.RegistrosPorPagina}");
        
        // Assert
        response.EnsureSuccessStatusCode(); // Fails the test if not 2xx
        response.Content.Headers.ContentType!.ToString().Should().Contain("application/json");

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }
}