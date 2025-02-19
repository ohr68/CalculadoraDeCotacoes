namespace CalculadoraDeCotacoes.Application.Dto;

public record SeguradoDto(
    string? Nome,
    int Ddd,
    int Telefone,
    string? Endereco,
    string? Cep,
    string? Documento,
    DateOnly DataNascimento,
    decimal Premio,
    decimal ImportanciaSegurada);