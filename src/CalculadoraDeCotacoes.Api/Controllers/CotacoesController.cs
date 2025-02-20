using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Cotacoes.AlterarCotacao;
using CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;
using CalculadoraDeCotacoes.Application.Cotacoes.ExcluirCotacao;
using CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;
using CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;
using CalculadoraDeCotacoes.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraDeCotacoes.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à cotações
/// </summary>
/// <param name="mediator"></param>
[Route("api/[controller]")]
public class CotacoesController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Lista cotações de um parceiro
    /// </summary>
    /// <param name="query">Modelo da Requisição de consulta com paginação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Lista paginada com as cotações</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedListResponse<ListarCotacoesPorParceiroResult>), StatusCodes.Status200OK,
        contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ListarCotacoes([FromQuery] ListarCotacoesPorParceiroQuery query,
        CancellationToken cancellationToken)
        => OkPaginated(await mediator.Send(query, cancellationToken));

    /// <summary>
    /// Obtém uma cotação através do id informado
    /// </summary>
    /// <param name="query">Modelo da Requisição de consulta cotendo o id da cotação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Detalhes da cotação</returns>
    [HttpGet("/detalhes")]
    [ProducesResponseType(typeof(DetalharCotacaoResult), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> DetalharCotacao([FromQuery] DetalharCotacaoQuery query,
        CancellationToken cancellationToken)
        => Ok(new ApiResponseWithData<DetalharCotacaoResult>(await mediator.Send(query, cancellationToken), true,
            "Cotação encontrada com sucesso."));

    /// <summary>
    /// Inclui uma nova cotação
    /// </summary>
    /// <param name="command">Modelo da Requisição de inclusão contendo os dados da nova cotação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de inclusão</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IncluirCotacaoResult), StatusCodes.Status201Created, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> IncluirCotacao([FromBody] IncluirCotacaoCommand command,
        CancellationToken cancellationToken)
        => Created(string.Empty,
            new ApiResponseWithData<IncluirCotacaoResult>(await mediator.Send(command, cancellationToken), true,
                "Cotação incluída com sucesso."));

    /// <summary>
    /// Altera uma cotação
    /// </summary>
    /// <param name="idCotacao">Id da cotação informado na rota</param>
    /// <param name="command">Modelo da Requisição de alteração contendo os dados da cotação alterada</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de alteração</returns>
    [HttpPut("{idCotacao:int}")]
    [ProducesResponseType(typeof(AlterarCotacaoResult), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> AlterarCotacao([FromRoute] int idCotacao, [FromBody] AlterarCotacaoCommand command,
        CancellationToken cancellationToken)
    {
        if (idCotacao != command.Id)
            throw new BadRequestException("O id informado na rota é diferente do id que está sendo alterado.");

        var resultado = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse(resultado.Sucesso, "Cotação alterada com sucesso."));
    }

    /// <summary>
    /// Exclui uma cotação através do id informado
    /// </summary>
    /// <param name="command">Modelo da Requisição de exclusão contendo o id da cotação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de exclusão</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(ExcluirCotacaoResult), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ExcluirCotacao([FromQuery] ExcluirCotacaoCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse(resultado.Sucesso, "Cotação excluída com sucesso."));
    }
}