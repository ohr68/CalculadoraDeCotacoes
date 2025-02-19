namespace CalculadoraDeCotacoes.Application.Beneficiarios.DetalharBeneficiario;

public class DetalharBeneficiarioResult
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Percentual { get; set; }
    public string? Parentesco { get; set; }

    public DetalharBeneficiarioResult()
    {
        
    }

    public DetalharBeneficiarioResult(int id, string? nome, int percentual, string? parentesco)
    {
        Id = id;
        Nome = nome;
        Percentual = percentual;
        Parentesco = parentesco;
    }
}