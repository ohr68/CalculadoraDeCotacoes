using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.AlterarCotacao;

public class AlterarCotacaoCommand : IRequest<AlterarCotacaoResult>
{
    public int Id { get; set; }
    public int IdProduto { get; set; }
    public int IdParceiro { get; set; }
    public string? NomeSegurado { get; set; }
    public int Ddd { get; set; }
    public int Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cep { get; set; }
    public string? Documento { get; set; }
    public DateOnly DataNascimento { get; set; }
    public decimal Premio { get; set; }
    public decimal ImportanciaSegurada { get; set; }
    public ICollection<BeneficiarioInputModel>? Beneficiarios { get; set; }
}