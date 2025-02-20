using CalculadoraDeCotacoes.Domain.Enums;

namespace CalculadoraDeCotacoes.Api.Requests;

public class BeneficiarioRequest
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Percentual { get; set; }
    public TipoParentesco IdParentesco { get; set; }
}