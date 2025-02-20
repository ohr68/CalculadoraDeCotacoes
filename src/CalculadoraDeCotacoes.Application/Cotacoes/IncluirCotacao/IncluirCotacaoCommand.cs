using CalculadoraDeCotacoes.Application.Models.InputModels;
using MediatR;

namespace CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;

public class IncluirCotacaoCommand : IRequest<IncluirCotacaoResult>
{
    public int IdProduto { get; set; }
    public string? NomeSegurado { get; set; }
    public int Ddd { get; set; }
    public int Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cep { get; set; }
    public string? Documento { get; set; }
    public DateOnly DataNascimento { get; set; }
    public decimal ImportanciaSegurada { get; set; }
    public ICollection<BeneficiarioInputModel>? Beneficiarios { get; set; }
    public ICollection<CoberturaInputModel>? Coberturas { get; set; }
}