namespace CalculadoraDeCotacoes.Application.Coberturas.ListarCoberturasPorCotacao;

public class ListarCoberturasPorCotacaoResult
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public int IdCobertura { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorAgravo { get; set; }
    public decimal ValorTotal { get; set; }
}