namespace CalculadoraDeCotacoes.Domain.Entities;

public class Cotacao
{
    public int Id { get; set; }
    public int IdProduto { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public int IdParceiro { get; set; }
    
    
    public virtual Produto? Produto { get; set; }
    public virtual Parceiro? Parceiro { get; set; }
    public virtual Segurado? Segurado { get; set; }
    public virtual ICollection<CotacaoBeneficiario>? CotacoesBeneficiarios { get; set; }
    public virtual ICollection<CotacaoCobertura>? CotacoesCoberturas { get; set; }
}