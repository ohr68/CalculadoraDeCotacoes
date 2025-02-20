namespace CalculadoraDeCotacoes.Application.Common;

public class CalculadoraDeIdade
{
    public static int ObterIdade(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idade = hoje.Year - dataNascimento.Year;

        // Ajuste caso o aniversário ainda não tenha ocorrido este ano
        if (dataNascimento > hoje.AddYears(idade))
            idade--;

        return idade;
    }
}