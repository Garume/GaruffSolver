using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllUnitPropagator : IUnitPropagator
{
    private readonly List<Clause> _unitClauses = new();

    public UnitPropagatorFactory UnitPropagation => (ref Formula formula, out Literal? literal) =>
    {
        literal = formula.Where(clause => clause.IsUnit).Select(x => x.Single()).FirstOrDefault();
        if (literal == null) return;

        _unitClauses.Clear();

        foreach (var clause in formula)
        {
            var hasLiteral = clause.Contains(literal.Value);

            if (clause.IsUnit || !hasLiteral)
            {
                var tempLiteral = literal;
                _unitClauses.Add(new Clause(clause.Where(literal => literal != tempLiteral.Value.Negative())));
            }
            else if (hasLiteral)
            {
            }
            else
            {
                _unitClauses.Add(clause);
            }
        }

        formula = new Formula(_unitClauses);
    };
}