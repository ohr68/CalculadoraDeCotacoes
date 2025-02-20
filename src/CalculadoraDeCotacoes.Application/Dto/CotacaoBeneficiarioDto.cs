namespace CalculadoraDeCotacoes.Application.Dto;

public record CotacaoBeneficiarioDto(
    int Id,
    string? Nome,
    int Percentual,
    int IdParentesco);