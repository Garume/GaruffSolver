using GaruffSolver.Values;

namespace GaruffSolver.Solver;

public interface ILiteralSelector
{
    LiteralSelectorFactory LiteralSelect { get; }
}

public delegate Literal LiteralSelectorFactory(Formula formula, Model model);