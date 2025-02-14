namespace CalculadoraDeCotacoes.Domain.Entities;

public class CotacaoBeneficiario
{
    public int Id { get; set; }
    public int IdCotacao { get; set; }
    public string? Nome { get; set; }
    public int Percentual { get; set; }
    public int IdParentesco { get; set; }
    
    public virtual Cotacao? Cotacao { get; set; }
    public virtual TipoParentesco? TipoParentesco { get; set; }
}