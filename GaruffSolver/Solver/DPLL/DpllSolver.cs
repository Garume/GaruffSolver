namespace GaruffSolver.Solver.DPLL;

public class DpllSolver : ISolver
{
    public BackTrackerFactory BackTrack { get; } = new DpllBackTracker().BackTrack;

    public PureLiteralEliminatorFactory PureLiteralElimination { get; } =
        new DpllPureLiteralEliminator().PureLiteralElimination;

    public UnitPropagatorFactory UnitPropagation { get; } = new DpllUnitPropagator().UnitPropagation;
    public LiteralSelectorFactory LiteralSelect { get; } = new DpllLiteralSelector().LiteralSelect;
}