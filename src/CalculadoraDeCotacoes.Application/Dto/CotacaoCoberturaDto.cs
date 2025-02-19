namespace CalculadoraDeCotacoes.Application.Dto;

public record CotacaoCoberturaDto(
    int Id,
    int IdCobertura,
    decimal ValorDesconto,
    decimal ValorAgravo,
    decimal ValorTotal);