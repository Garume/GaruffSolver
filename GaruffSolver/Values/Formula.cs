using GaruffSolver.CNF;

namespace GaruffSolver.Values;

public class Formula : HashSet<Clause>, IEquatable<Formula>
{
    public Formula()
    {
    }

    public Formula(Cnf cnf)
    {
        foreach (var clause in cnf.Clauses)
        {
            var literals = new List<Literal>();
            foreach (var (name, isPositive) in clause) literals.Add(new Literal(name, isPositive));
            Add(new Clause(literals));
        }
    }

    public Formula(IEnumerable<Clause> clauses) : base(clauses)
    {
    }

    public bool Equals(Formula? other)
    {
        return other != null && Count == other.Count && other.All(Contains);
    }

    public override bool Equals(object? obj)
    {
        return obj is Formula other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var clause in this) hash.Add(clause);

        return hash.ToHashCode();
    }

    public Formula UnitPropagation()
    {
        var unitLiteral = this.Where(clause => clause.IsUnit).Select(x => x.Single()).FirstOrDefault();
        if (unitLiteral == null) return this;

        var unitClauses = new List<Clause>();

        foreach (var clause in this)
            if (clause.IsUnit || !clause.Contains(unitLiteral))
            {
                unitClauses.Add(new Clause(clause.Where(literal => literal != unitLiteral.Negative())));
            }
            else if (clause.Contains(unitLiteral))
            {
            }
            else
            {
                unitClauses.Add(clause);
            }

        return new Formula(unitClauses);
    }

    public Formula PureLiteralElimination()
    {
        var literals = this.SelectMany(c => c).Distinct().ToArray();
        var pureLiterals = literals.Where(l => l.IsPure(literals)).ToList();

        return new Formula(this.Where(clause => !pureLiterals.Any(clause.Contains)));
    }


    public override string ToString()
    {
        return string.Join(" ∧ ", this.Select(c => c.ToString()));
    }
}