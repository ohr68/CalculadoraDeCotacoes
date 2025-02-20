namespace CalculadoraDeCotacoes.Domain.Entities;

public class TipoParentesco
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    
    public virtual ICollection<CotacaoBeneficiario>? CotacoesBeneficiarios { get; set; }
}