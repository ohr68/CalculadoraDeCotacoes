namespace CalculadoraDeCotacoes.Domain.Entities;

public class Segurado
{
    public int CotacaoId { get; set; }
    public string? NomeSegurado { get; set; }
    public int Ddd { get; set; }
    public int Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cep { get; set; }
    public string? Documento { get; set; }
    public DateOnly DataNascimento { get; set; }
    public decimal Premio { get; set; }
    public decimal ImportanciaSegurada { get; set; }
    
    public virtual Cotacao? Cotacao { get; set; }
}