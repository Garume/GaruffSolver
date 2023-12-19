namespace GaruffSolver.Solver;

public sealed class SolveBuilder
{
    private readonly IBackTracker _backTracker;
    private readonly ILiteralSelector _literalSelector;
    private readonly IPureLiteralEliminator _pureLiteralEliminator;
    private readonly IUnitPropagator _unitPropagator;

    public SolveBuilder(ISolver solver)
    {
        _unitPropagator = solver;
        _pureLiteralEliminator = solver;
        _literalSelector = solver;
        _backTracker = solver;
    }

    public SolveBuilder(IUnitPropagator unitPropagator, IPureLiteralEliminator pureLiteralEliminator,
        ILiteralSelector literalSelector, IBackTracker backTracker)
    {
        _unitPropagator = unitPropagator;
        _pureLiteralEliminator = pureLiteralEliminator;
        _literalSelector = literalSelector;
        _backTracker = backTracker;
    }

    public UnitPropagatorFactory UnitPropagation => _unitPropagator.UnitPropagation;
    public PureLiteralEliminatorFactory PureLiteralElimination => _pureLiteralEliminator.PureLiteralElimination;
    public LiteralSelectorFactory LiteralSelect => _literalSelector.LiteralSelect;
    public BackTrackerFactory BackTrack => _backTracker.BackTrack;
}