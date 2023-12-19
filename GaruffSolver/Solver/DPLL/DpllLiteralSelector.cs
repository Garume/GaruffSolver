namespace GaruffSolver.Solver.DPLL;

public sealed class DpllLiteralSelector : ILiteralSelector
{
    public LiteralSelectorFactory LiteralSelect => (formula, model) =>
    {
        return formula.SelectMany(clause => clause).First();
    };
}