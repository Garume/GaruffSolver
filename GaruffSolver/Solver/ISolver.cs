namespace GaruffSolver.Solver;

public interface ISolver : IPureLiteralEliminator, IUnitPropagator, IBackTracker, ILiteralSelector
{
}