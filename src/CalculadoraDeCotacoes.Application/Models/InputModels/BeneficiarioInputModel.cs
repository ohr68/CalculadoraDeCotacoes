using CalculadoraDeCotacoes.Domain.Enums;

namespace CalculadoraDeCotacoes.Application.Models.InputModels;

public class BeneficiarioInputModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Percentual { get; set; }
    public TipoParentesco IdParentesco { get; set; }
}