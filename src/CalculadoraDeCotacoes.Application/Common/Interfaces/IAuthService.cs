namespace CalculadoraDeCotacoes.Application.Common.Interfaces;

public interface IAuthService
{
    public Task<int> ObterIdParceiro(CancellationToken cancelToken);
}