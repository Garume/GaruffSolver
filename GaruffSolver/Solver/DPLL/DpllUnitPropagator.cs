using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllUnitPropagator : IUnitPropagator
{
    private readonly List<Clause> _unitClauses = new();

    public UnitPropagatorFactory UnitPropagation => Propagation;

    private void Propagation(ref Formula formula, out Literal? literal)
    {
        literal = formula.Where(clause => clause.IsUnit).Select(x => x.Single()).FirstOrDefault();
        if (literal == null) return;

        _unitClauses.Clear();

        foreach (var clause in formula)
        {
            var hasLiteral = false;

            foreach (var l in clause)
                if (l.Equals(literal))
                {
                    hasLiteral = true;
                    break;
                }

            if (clause.Count == 1 || !hasLiteral)
            {
                var emptyClause = new Clause();

                foreach (var l in clause)
                    if (!l.Equals(literal.Value.Negative()))
                        emptyClause.AddLast(l);

                _unitClauses.Add(emptyClause);
            }
            else if (hasLiteral)
            {
            }
            else
            {
                _unitClauses.Add(clause);
            }
        }

        formula.Clear();
        foreach (var clause in _unitClauses)
        {
            formula.Add(clause);
        }
    }
}