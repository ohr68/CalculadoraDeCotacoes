namespace CalculadoraDeCotacoes.Application.Dto;

public record ProdutoDto(int Id, string? Descricao, decimal ValorBase, decimal Limite);