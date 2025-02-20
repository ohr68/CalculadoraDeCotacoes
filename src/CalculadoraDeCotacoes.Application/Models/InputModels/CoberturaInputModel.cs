using CalculadoraDeCotacoes.Domain.Enums;

namespace CalculadoraDeCotacoes.Application.Models.InputModels;

public class CoberturaInputModel
{
    public int IdCobertura { get; set; }
    public TipoCobertura TipoCobertura { get; set; }
}