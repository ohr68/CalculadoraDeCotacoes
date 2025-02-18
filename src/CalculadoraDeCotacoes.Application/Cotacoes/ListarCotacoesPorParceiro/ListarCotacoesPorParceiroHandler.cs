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
    IValidator<ListarCotacoesPorParceiroQuery> requisicaoValidator,
    IValidator<RequisicaoPaginadaInputModel> requisicaoPaginadaValidator)
    : IRequestHandler<ListarCotacoesPorParceiroQuery, IQueryable<ListarCotacoesPorParceiroResult>>
{
    public async Task<IQueryable<ListarCotacoesPorParceiroResult>> Handle(ListarCotacoesPorParceiroQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await requisicaoValidator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var paginacaoValidationResult = await requisicaoPaginadaValidator.ValidateAsync(request, cancellationToken);
        
        if(!paginacaoValidationResult.IsValid)
            throw new ValidationException(paginacaoValidationResult.Errors);
        
        var cotacoes = await context.Cotacoes
            .Include(c => c.Produto)
            .Include(c => c.Segurado)
            .Where(c => c.IdParceiro == request.IdParceiro)
            .ToListAsync(cancellationToken);

        if (cotacoes.Count == 0)
            throw new NotFoundException("Nenhuma cotação encontrada para o parceiro informado.");

        return cotacoes.Adapt<IQueryable<ListarCotacoesPorParceiroResult>>();
    }
}