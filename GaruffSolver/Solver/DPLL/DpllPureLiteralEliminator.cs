using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllPureLiteralEliminator : IPureLiteralEliminator
{
    public PureLiteralEliminatorFactory PureLiteralElimination =>
        LiteralElimination;

    private Dictionary<ushort, (bool positive, bool negative, Literal literal)> _literalAppearances =
        new();
    
    private void LiteralElimination(ref Formula formula, out HashSet<Literal> pureLiterals)
    {
        pureLiterals = new HashSet<Literal>();

        _literalAppearances.Clear();
        // リテラルの出現を追跡
        foreach (var clause in formula)
        foreach (var literal in clause)
        {
            if (!_literalAppearances.TryGetValue(literal.Value, out var appearance)) appearance = (false, false, literal);

            if (literal.IsPositive)
                appearance.positive = true;
            else
                appearance.negative = true;

            _literalAppearances[literal.Value] = appearance;
        }

        // 純リテラルを識別し、論理式を単純化
        foreach (var (_, appearance) in _literalAppearances)
            if (appearance.positive != appearance.negative) // 純リテラル
            {
                pureLiterals.Add(appearance.literal);
                formula.RemoveAll(clause => clause.Contains(appearance.literal));
            }
    }
}