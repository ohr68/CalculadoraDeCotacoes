using CalculadoraDeCotacoes.Application.Dto;

namespace CalculadoraDeCotacoes.Application.Cotacoes.DetalharCotacao;

public class DetalharCotacaoResult
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    
    public virtual ProdutoDto? Produto { get; set; }
    public virtual SeguradoDto? Segurado { get; set; }
    public virtual ICollection<CotacaoBeneficiarioDto>? Beneficiarios { get; set; }
    public virtual ICollection<CotacaoCoberturaDto>? Coberturas { get; set; }
}