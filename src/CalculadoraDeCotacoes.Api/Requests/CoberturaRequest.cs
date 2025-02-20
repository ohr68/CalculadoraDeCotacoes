using CalculadoraDeCotacoes.Domain.Enums;

namespace CalculadoraDeCotacoes.Api.Requests;

public class CoberturaRequest
{
    public int IdCobertura { get; set; }
    public TipoCobertura TipoCobertura { get; set; }
}