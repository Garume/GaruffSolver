using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllBackTracker : IBackTracker
{
    public BackTrackerFactory BackTrack => (formula, literal, model, solve) =>
    {
        foreach (var value in new[] { true, false })
        {
            var newModel = new Model(model);
            newModel.Assign(literal.Name, value);

            var newFormula = new Formula(formula);
            var newLiteral = new Literal(literal.Name, value);
            newFormula.AddLast(new Clause(new[] { newLiteral }));

            var resultModel = solve(newFormula, newModel);
            if (resultModel.IsSatisfied) return resultModel;
        }

        return model;
    };
}