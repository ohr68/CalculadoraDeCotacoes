using FluentValidation;

namespace CalculadoraDeCotacoes.Domain.Entities;

public class Segurado
{
    public int CotacaoId { get; set; }
    public string? Nome { get; set; }
    public int Ddd { get; set; }
    public int Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cep { get; set; }
    public string? Documento { get; set; }
    public DateOnly DataNascimento { get; set; }
    public decimal Premio { get; set; }
    public decimal ImportanciaSegurada { get; set; }

    public virtual Cotacao? Cotacao { get; set; }

    public void CalcularValorPremio(decimal valorBase, Cotacao cotacao)
        => Premio = valorBase + cotacao.CotacoesCoberturas!.Sum(c => c.ValorTotal);

    public void VerificarValorImportanciaSegurada(decimal limite)
    {
        if (ImportanciaSegurada > limite)
            throw new ValidationException("O valor da IS está fora do limite permitido para o produto informado.");
    }
}