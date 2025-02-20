namespace CalculadoraDeCotacoes.Domain.Entities;

public class FaixaDeIdade
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public int Desconto { get; set; }
    public int Agravo { get; set; }
}