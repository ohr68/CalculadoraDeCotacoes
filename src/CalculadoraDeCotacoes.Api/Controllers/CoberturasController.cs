using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Coberturas.ExcluirCobertura;
using CalculadoraDeCotacoes.Application.Coberturas.IncluirCobertura;
using CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraDeCotacoes.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à coberturas
/// </summary>
/// <param name="mediator"></param>
[Route("api/[controller]")]
public class CoberturasController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Lista coberturas de uma cotação
    /// </summary>
    /// <param name="query">Modelo da Requisição de consulta com paginação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Lista paginada com as coberturas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedListResponse<ListarCoberturasPorCotacaoResult>), StatusCodes.Status200OK,
        contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ListarCoberturasPorCotacao([FromQuery] ListarCoberturasPorCotacaoQuery query,
        CancellationToken cancellationToken)
        => OkPaginated(await mediator.Send(query, cancellationToken));

    /// <summary>
    /// Inclui uma nova cobertura para a cotação informada
    /// </summary>
    /// <param name="command">Modelo da Requisição de inclusão contendo os dados da nova cobertura</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de inclusão</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<IncluirCoberturaResult>), StatusCodes.Status201Created,
        contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> IncluirCobertura([FromBody] IncluirCoberturaCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await mediator.Send(command, cancellationToken);

        return Created(string.Empty,
            new ApiResponseWithData<IncluirCoberturaResult>(resultado, true, "Cobertura incluída com sucesso."));
    }

    /// <summary>
    /// Exclui uma cobertura através do id da cotação e o id da cobertura
    /// </summary>
    /// <param name="command">Modelo da Requisição de exclusão contendo o id da cotação e o id da cobertura</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de exclusão</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ExcluirCobertura([FromQuery] ExcluirCoberturaCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse(resultado.Sucesso, "Cobertura excluída com sucesso."));
    }
}