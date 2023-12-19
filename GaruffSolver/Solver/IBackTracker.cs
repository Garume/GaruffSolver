using GaruffSolver.Values;

namespace GaruffSolver.Solver;

public interface IBackTracker
{
    BackTrackerFactory BackTrack { get; }
}

public delegate Model BackTrackerFactory(Formula formula, Literal literal, Model model,
    Func<Formula, Model, Model> solve);