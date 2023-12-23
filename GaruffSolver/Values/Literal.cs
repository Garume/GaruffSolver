namespace GaruffSolver.Values;

public struct Literal : IComparable<Literal>
{
    public ushort Value { get; }
    public bool IsPositive { get; private init; }

    public Literal(ushort name, bool isPositive)
    {
        Value = name;
        IsPositive = isPositive;
    }

    public int CompareTo(Literal other)
    {
        var nameComparison = Value.CompareTo(other.Value);
        if (nameComparison != 0) return nameComparison;
        return IsPositive.CompareTo(other.IsPositive);
    }


    public bool Equals(Literal? other)
    {
        return other != null && Value == other.Value.Value &&
               IsPositive == other.Value.IsPositive;
    }

    public Literal Negative()
    {
        return this with { IsPositive = !IsPositive };
    }

    public static Literal Of(ushort name)
    {
        return new Literal(name, true);
    }

    public override string ToString()
    {
        return (IsPositive ? "" : "-") + Value;
    }


    public bool IsPure(List<Literal> literals)
    {
        
        var isPure = true;
        foreach (var literal in literals)
        {
            if (Value != literal.Value) continue;
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
            return Value.GetHashCode() * IsPositive.GetHashCode();
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