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
            _allLiterals.Clear();
            foreach (var clause in formula) _allLiterals.AddRange(clause);

            var pureLiteralsSet = new HashSet<Literal>();
            foreach (var literal in _allLiterals)
                if (literal.IsPure(_allLiterals))
                    pureLiteralsSet.Add(literal);
            pureLiterals = pureLiteralsSet;

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

            formula = new Formula(_newFormula);
        };
}