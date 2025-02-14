namespace CalculadoraDeCotacoes.Domain.Entities;

public class CotacaoCobertura
{
    public int Id { get; set; }
    public int IdCotacao { get; set; }
    public int IdCobertura { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorAgravo { get; set; }
    public decimal ValorTotal { get; set; }
    
    public virtual Cotacao? Cotacao { get; set; }
    public virtual Cobertura? Cobertura { get; set; }
}