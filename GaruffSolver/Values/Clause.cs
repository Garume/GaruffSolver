namespace GaruffSolver.Values;

public class Clause : HashSet<Literal>, IEquatable<Clause>
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
        var hash = new HashCode();
        foreach (var literal in this) hash.Add(literal);

        return hash.ToHashCode();
    }


    public override string ToString()
    {
        var value = string.Join(" v ", this.Select(l => l.ToString()));
        return $"( {value} )";
    }

    public Clause Without(Literal literal)
    {
        return new Clause(this.Where(x => x != literal));
    }
}