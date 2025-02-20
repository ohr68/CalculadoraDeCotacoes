namespace CalculadoraDeCotacoes.Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public decimal ValorBase { get; set; }
    public decimal Limite { get; set; }
    
    public virtual ICollection<Cotacao>? Cotacoes { get; set; }
}