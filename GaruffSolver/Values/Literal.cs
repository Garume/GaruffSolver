namespace GaruffSolver.Values;

public struct Literal : IComparable<Literal>
{
    public string Name { get; }
    public bool IsPositive { get; private init; }

    public Literal(string name, bool isPositive)
    {
        Name = name;
        IsPositive = isPositive;
    }

    public int CompareTo(Literal other)
    {
        var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
        if (nameComparison != 0) return nameComparison;
        return IsPositive.CompareTo(other.IsPositive);
    }

    public int CompareTo(Literal? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var nameComparison = string.Compare(Name, other.Value.Name, StringComparison.Ordinal);
        if (nameComparison != 0) return nameComparison;
        return IsPositive.CompareTo(other.Value.IsPositive);
    }

    public bool Equals(Literal? other)
    {
        return other != null && string.Equals(Name, other.Value.Name, StringComparison.Ordinal) &&
               IsPositive == other.Value.IsPositive;
    }

    public Literal Negative()
    {
        return this with { IsPositive = !IsPositive };
    }

    public static Literal Of(string name)
    {
        return new Literal(name, true);
    }

    public override string ToString()
    {
        return (IsPositive ? "" : "-") + Name;
    }


    public bool IsPure(IEnumerable<Literal> literals)
    {
        var isPure = true;
        foreach (var literal in literals)
        {
            if (Name != literal.Name) continue;
            if (IsPositive == literal.IsPositive) continue;
            isPure = false;
            break;
        }

        return isPure;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return Name.GetHashCode() * IsPositive.GetHashCode();
        }
    }

    #region operator overloads

    public static implicit operator Clause(Literal literal)
    {
        var newClause = new Clause();
        newClause.AddLast(literal);
        return newClause;
    }

    public static Literal operator -(Literal literal)
    {
        return literal.Negative();
    }


    public static Clause operator |(Literal literal1, Literal literal2)
    {
        return new Clause(new[] { literal1, literal2 });
    }

    public static Formula operator &(Literal literal1, Literal literal2)
    {
        return new Formula(new[] { new Clause(new[] { literal1 }), new Clause(new[] { literal2 }) });
    }

    #endregion
}