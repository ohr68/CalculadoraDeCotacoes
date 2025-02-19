using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;

public class ListarBeneficiariosPorCotacaoHandler(
    ApplicationDbContext context,
    IValidator<ListarBeneficiariosPorCotacaoQuery> listarBeneficiariosValidator,
    IValidator<RequisicaoPaginadaInputModel> requisicaoPaginadaValidator)
    : IRequestHandler<ListarBeneficiariosPorCotacaoQuery, PaginatedList<ListarBeneficiariosPorCotacaoResult>>
{
    public async Task<PaginatedList<ListarBeneficiariosPorCotacaoResult>> Handle(
        ListarBeneficiariosPorCotacaoQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await listarBeneficiariosValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginacaoValidationResult = await requisicaoPaginadaValidator.ValidateAsync(request, cancellationToken);

        if (!paginacaoValidationResult.IsValid)
            throw new ValidationException(paginacaoValidationResult.Errors);

        var beneficiarios = await context.CotacoesBeneficiarios
            .Include(cb => cb.TipoParentesco)
            .Where(cb => cb.IdCotacao == request.IdCotacao)
            .ToListAsync(cancellationToken);

        if (beneficiarios is null || beneficiarios.Count <= 0)
            throw new NotFoundException($"Nenhum beneficiario encontrado para a cotação de id {request.IdCotacao}.");

        return await PaginatedList<ListarBeneficiariosPorCotacaoResult>.CreateAsync(
            beneficiarios.Adapt<IQueryable<ListarBeneficiariosPorCotacaoResult>>(), request.Pagina,
            request.RegistrosPorPagina, cancellationToken);
    }
}