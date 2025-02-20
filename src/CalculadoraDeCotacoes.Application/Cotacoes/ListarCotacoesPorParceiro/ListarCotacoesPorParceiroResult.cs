namespace CalculadoraDeCotacoes.Application.Cotacoes.ListarCotacoesPorParceiro;

public class ListarCotacoesPorParceiroResult
{
    public int IdCotacao { get; set; }
    public string? NomeDoSegurado { get; set; }
    public string? DocumentoDoSegurado { get; set; }
    public string? NomeDoProduto { get; set; }

    public ListarCotacoesPorParceiroResult()
    {
        
    }

    public ListarCotacoesPorParceiroResult(int idCotacao, string? nomeDoSegurado, string? documentoDoSegurado, string? nomeDoProduto)
    {
        IdCotacao = idCotacao;
        NomeDoSegurado = nomeDoSegurado;
        DocumentoDoSegurado = documentoDoSegurado;
        NomeDoProduto = nomeDoProduto;
    }
}