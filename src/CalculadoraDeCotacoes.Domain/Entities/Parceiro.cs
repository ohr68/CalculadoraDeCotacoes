namespace CalculadoraDeCotacoes.Domain.Entities;

public class Parceiro
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public string? Secret { get; set; }

    public virtual ICollection<Cotacao>? Cotacoes { get; set; }
}