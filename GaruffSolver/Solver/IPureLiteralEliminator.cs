using GaruffSolver.Values;

namespace GaruffSolver.Solver;

public interface IPureLiteralEliminator
{
    public PureLiteralEliminatorFactory PureLiteralElimination { get; }
}

public delegate void PureLiteralEliminatorFactory(ref Formula formula, out HashSet<Literal> literals);