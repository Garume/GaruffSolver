namespace GaruffSolver.Solver.DPLL;

public sealed class DpllLiteralSelector : ILiteralSelector
{
    public LiteralSelectorFactory LiteralSelect => (formula, model) => formula.First().First();
}