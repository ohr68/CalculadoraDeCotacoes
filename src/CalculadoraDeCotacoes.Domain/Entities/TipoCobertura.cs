namespace CalculadoraDeCotacoes.Domain.Entities;

public class TipoCobertura
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    
    public virtual ICollection<Cobertura>? Coberturas { get; set; }
}