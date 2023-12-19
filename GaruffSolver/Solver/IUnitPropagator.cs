using GaruffSolver.Values;

namespace GaruffSolver.Solver;

public interface IUnitPropagator
{
    UnitPropagatorFactory UnitPropagation { get; }
}

public delegate void UnitPropagatorFactory(ref Formula formula, out Literal? literal);