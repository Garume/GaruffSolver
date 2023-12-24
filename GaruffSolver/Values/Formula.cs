using GaruffSolver.CNF;

namespace GaruffSolver.Values;

public class Formula : List<Clause>, IEquatable<Formula>
{
    private readonly List<Literal> _allLiterals = new();

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


    public IEnumerable<ushort> GetVariables()
    {
        return this.SelectMany(clause => clause).Select(literal => literal.Value).Distinct();
    }

    public override bool Equals(object? obj)
    {
        return obj is Formula other && Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Aggregate(397, (current, clause) => current ^ clause.GetHashCode());
    }

    public override string ToString()
    {
        return string.Join(" ∧ ", this.Select(c => c.ToString()));
    }

    #region operator overloads

    public static Formula operator &(Formula formula, Literal literal)
    {
        var newFormula = new Formula(formula);
        newFormula.Add(literal);
        return newFormula;
    }

    public static Formula operator &(Formula formula, Clause clause)
    {
        var newFormula = new Formula(formula);
        newFormula.Add(clause);
        return newFormula;
    }

    #endregion
}