using CalculadoraDeCotacoes.Api.Common;
using CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;
using CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;
using CalculadoraDeCotacoes.Application.Beneficiarios.ExcluirBeneficiario;
using CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;
using CalculadoraDeCotacoes.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraDeCotacoes.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à beneficiários
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/[controller]")]
public class BeneficiariosController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Lista beneficiários de uma cotação
    /// </summary>
    /// <param name="query">Modelo da Requisição de consulta com paginação</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Lista paginada com os beneficiários</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedListResponse<ListarBeneficiariosPorCotacaoResult>), StatusCodes.Status200OK,
        contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ListarBeneficiariosPorCotacao([FromQuery] ListarBeneficiariosPorCotacaoQuery query,
        CancellationToken cancellationToken)
        => OkPaginated(await mediator.Send(query, cancellationToken));


    /// <summary>
    /// Obtém um beneficiário através do id da cotação e do id do beneficiário
    /// </summary>
    /// <param name="query">Modelo da Requisição de consulta cotendo o id da cotação e o id do beneficiário</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Detalhes do beneficiário</returns>
    [HttpGet("/detalhes-beneficiario")]
    [ProducesResponseType(typeof(ApiResponseWithData<DetalharBeneficiarioResult>), StatusCodes.Status200OK,
        contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> DetalharBeneficiario([FromQuery] DetalharBeneficiarioQuery query,
        CancellationToken cancellationToken)
        => Ok(new ApiResponseWithData<DetalharBeneficiarioResult>(await mediator.Send(query, cancellationToken), true,
            "Beneficiário encontrado com sucesso."));

    /// <summary>
    /// Altera um beneficiário
    /// </summary>
    /// <param name="idCotacao">Id da cotação informado na rota</param>
    /// <param name="command">Modelo da Requisição de alteração contendo os beneficiários alterados, incluídos ou removidos</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de alteração</returns>
    [HttpPut("{idCotacao:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> AlterarBeneficiario([FromRoute] int idCotacao,
        [FromBody] AlterarBeneficiarioCommand command, CancellationToken cancellationToken)
    {
        if (idCotacao != command.IdCotacao)
            throw new BadRequestException("O id informado na rota é diferente do id que está sendo alterado.");

        var resultado = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse(resultado.Sucesso, "Alterações concluídas com sucesso."));
    }

    /// <summary>
    /// Exclui um beneficiário através do id da cotação e do id do beneficiário
    /// </summary>
    /// <param name="command">Modelo da Requisição de exclusão contendo o id da cotação e do beneficiário</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resultado da operação de exclusão</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity,
        contentType: "application/json")]
    public async Task<IActionResult> ExcluirBeneficiario([FromQuery] ExcluirBeneficiarioCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse(resultado.Sucesso, "Beneficiário excluído com sucesso."));
    }
}