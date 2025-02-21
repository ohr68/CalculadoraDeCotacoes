using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;

public class ListarCoberturasPorCotacaoHandler(
    ApplicationDbContext context,
    IValidator<ListarCoberturasPorCotacaoQuery> listarCoberturasValidator,
    IValidator<RequisicaoPaginadaInputModel> requisicaoPaginadaValidator)
    : IRequestHandler<ListarCoberturasPorCotacaoQuery, PaginatedList<ListarCoberturasPorCotacaoResult>>
{
    public async Task<PaginatedList<ListarCoberturasPorCotacaoResult>> Handle(ListarCoberturasPorCotacaoQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await listarCoberturasValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginacaoValidationResult = await requisicaoPaginadaValidator.ValidateAsync(request, cancellationToken);

        if (!paginacaoValidationResult.IsValid)
            throw new ValidationException(paginacaoValidationResult.Errors);

        var coberturas = await context.CotacoesCoberturas.Where(c => c.IdCotacao == request.IdCotacao)
            .ToListAsync(cancellationToken);

        if (coberturas.Count == 0)
            throw new NotFoundException($"Nenhuma cobertura encontrada para a cotação de id {request.IdCotacao}.");

        return PaginatedList<ListarCoberturasPorCotacaoResult>.Create(
            coberturas.Adapt<IEnumerable<ListarCoberturasPorCotacaoResult>>(), request.Pagina,
            request.RegistrosPorPagina, cancellationToken);
    }
}