using CalculadoraDeCotacoes.Application.Common.Constants;
using CalculadoraDeCotacoes.Application.Common.Interfaces;
using CalculadoraDeCotacoes.Domain.Exceptions;
using CalculadoraDeCotacoes.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Common;

public class AuthService(ApplicationDbContext context, IHttpContextAccessor contextAccessor) : IAuthService
{
    public async Task<int> ObterIdParceiro(CancellationToken cancellationToken)
    {
        var parceiroValido = await context.Parceiros.AnyAsync(cancellationToken);

        if (!parceiroValido)
            throw new ForbiddenException($"Id do parceiro inválido.");

        return contextAccessor.HttpContext.Items.TryGetValue(Auth.IdParceiro, out var idParceiro)
            ? Convert.ToInt32(idParceiro)
            : 0;
    }
}