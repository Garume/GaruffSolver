using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllPureLiteralEliminator : IPureLiteralEliminator
{
    // メンバとしてのリスト
    private readonly List<Literal> _allLiterals = new();
    private readonly List<Clause> _newFormula = new();

    public PureLiteralEliminatorFactory PureLiteralElimination =>
        (ref Formula formula, out HashSet<Literal> pureLiterals) =>
        {
            pureLiterals = formula.GetPureLiterals();

            _newFormula.Clear();
            foreach (var clause in formula)
            {
                var containsPureLiteral = false;
                foreach (var literal in pureLiterals)
                    if (clause.Contains(literal))
                    {
                        containsPureLiteral = true;
                        break;
                    }

                if (!containsPureLiteral) _newFormula.Add(clause);
            }

            formula.Clear();
            foreach (var clause in _newFormula) formula.AddLast(clause);
        };
}