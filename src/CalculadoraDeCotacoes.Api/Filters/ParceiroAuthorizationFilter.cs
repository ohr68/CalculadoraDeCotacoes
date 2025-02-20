using CalculadoraDeCotacoes.Application.Common.Constants;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Api.Filters;

public class ParceiroAuthorizationFilter(ApplicationDbContext dbContext) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;

        if (!request.Headers.TryGetValue(Auth.Secret, out var secret))
            throw new UnauthorizedException("É obrigatório informar o secret do parceiro.");

        var parceiro = await dbContext.Parceiros.FirstOrDefaultAsync(p => p.Secret == secret) ??
                       throw new ForbiddenException("Secret inválido.");

        context.HttpContext.Items[Auth.IdParceiro] = parceiro.Id;
        
        await Task.CompletedTask;
    }
}