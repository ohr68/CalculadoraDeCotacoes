namespace CalculadoraDeCotacoes.Domain.Entities;

public class Cobertura
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public int TipoCoberturaId { get; set; }
    public decimal Valor { get; set; }
    
    public virtual TipoCobertura? TipoCobertura { get; set; }
}