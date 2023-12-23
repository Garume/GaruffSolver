namespace GaruffSolver.Values;

public class Clause : LinkedList<Literal>, IEquatable<Clause>
{
    public Clause() : this(new List<Literal>())
    {
    }

    public Clause(IEnumerable<Literal> literals) : base(literals)
    {
    }

    public bool IsUnit => Count == 1;

    public bool IsEmpty => Count == 0;

    public bool Equals(Clause? other)
    {
        return other != null && Count == other.Count && other.All(Contains);
    }

    public override bool Equals(object? obj)
    {
        return obj is Clause other && Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Aggregate(397, (current, literal) => current ^ literal.GetHashCode());
    }

    public override string ToString()
    {
        var value = string.Join(" v ", this.Select(l => l.ToString()));
        return $"( {value} )";
    }

    #region operator overloads

    public static Formula operator &(Clause clause1, Clause clause2)
    {
        return new Formula(new[] { clause1, clause2 });
    }

    public static Clause operator |(Clause clause, Literal literal)
    {
        var newClause = new Clause(clause);
        newClause.AddLast(literal);
        return newClause;
    }

    #endregion
}