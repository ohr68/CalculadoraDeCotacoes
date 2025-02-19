namespace CalculadoraDeCotacoes.Application.Beneficiarios.ListarBeneficiariosPorCotacao;

public class ListarBeneficiariosPorCotacaoResult
{
    public string? Nome { get; set; }
    public string? TipoParentesco { get; set; }
    public int PercentualParticipacao { get; set; }

    public ListarBeneficiariosPorCotacaoResult()
    {
        
    }

    public ListarBeneficiariosPorCotacaoResult(string? nome, string? tipoParentesco, int percentualParticipacao)
    {
        Nome = nome;
        TipoParentesco = tipoParentesco;
        PercentualParticipacao = percentualParticipacao;
    }
}