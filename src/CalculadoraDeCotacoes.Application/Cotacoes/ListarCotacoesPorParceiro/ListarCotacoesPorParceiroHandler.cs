using CalculadoraDeCotacoes.Application.Common;
using CalculadoraDeCotacoes.Application.Common.Interfaces;
using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;

public class ListarCotacoesPorParceiroHandler(
    ApplicationDbContext context,
    IValidator<RequisicaoPaginadaInputModel> requisicaoPaginadaValidator,
    IAuthService authService)
    : IRequestHandler<ListarCotacoesPorParceiroQuery, PaginatedList<ListarCotacoesPorParceiroResult>>
{
    public async Task<PaginatedList<ListarCotacoesPorParceiroResult>> Handle(ListarCotacoesPorParceiroQuery request,
        CancellationToken cancellationToken)
    {
        var idParceiro = await authService.ObterIdParceiro(cancellationToken);
        
        var paginacaoValidationResult = await requisicaoPaginadaValidator.ValidateAsync(request, cancellationToken);

        if (!paginacaoValidationResult.IsValid)
            throw new ValidationException(paginacaoValidationResult.Errors);

        var cotacoes = await context.Cotacoes
            .Include(c => c.Produto)
            .Include(c => c.Segurado)
            .Where(c => c.IdParceiro == idParceiro)
            .ToListAsync(cancellationToken);

        if (cotacoes.Count == 0)
            throw new NotFoundException("Nenhuma cotação encontrada para o parceiro informado.");

        return PaginatedList<ListarCotacoesPorParceiroResult>.Create(
            cotacoes.Adapt<IEnumerable<ListarCotacoesPorParceiroResult>>(), request.Pagina, request.RegistrosPorPagina,
            cancellationToken);
    }
}