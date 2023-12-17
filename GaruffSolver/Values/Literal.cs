namespace GaruffSolver.Values;

public record Literal(string Name, bool IsPositive)
{
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

    public bool ConflictsWith(Literal literal)
    {
        return string.Equals(Name, literal.Name) && IsPositive != literal.IsPositive;
    }

    /// <summary>
    ///     Checks whether this Literal conflicts with any of the given <paramref name="literals" />.
    /// </summary>
    public bool IsPure(IEnumerable<Literal> literals)
    {
        return !literals.Any(ConflictsWith);
    }
}