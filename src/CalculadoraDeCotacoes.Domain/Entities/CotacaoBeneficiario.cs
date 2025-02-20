namespace CalculadoraDeCotacoes.Domain.Entities;

public class CotacaoBeneficiario : IEquatable<CotacaoBeneficiario>
{
    public int Id { get; set; }
    public int IdCotacao { get; set; }
    public string? Nome { get; set; }
    public int Percentual { get; set; }
    public int IdParentesco { get; set; }

    public virtual Cotacao? Cotacao { get; set; }
    public virtual TipoParentesco? TipoParentesco { get; set; }

    public bool Equals(CotacaoBeneficiario? other)
        => other is not null && (ReferenceEquals(this, other) || Id == other.Id && IdCotacao == other.IdCotacao &&
            Nome == other.Nome &&
            Percentual == other.Percentual &&
            IdParentesco == other.IdParentesco && Equals(Cotacao, other.Cotacao) &&
            Equals(TipoParentesco, other.TipoParentesco));
    
    public override bool Equals(object? obj)
        => obj is not null &&
           (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((CotacaoBeneficiario)obj));
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, IdCotacao, Nome, Percentual, IdParentesco, Cotacao, TipoParentesco);
    }
}