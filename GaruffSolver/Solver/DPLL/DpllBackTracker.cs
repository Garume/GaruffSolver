using GaruffSolver.Values;

namespace GaruffSolver.Solver.DPLL;

public sealed class DpllBackTracker : IBackTracker
{
    private readonly Clause _unitClause = new ();
    private readonly bool[] _table = new[] { true, false };
    
    public BackTrackerFactory BackTrack => Track;

    private Model Track(Formula formula, Literal literal, Model model, Func<Formula, Model, Model> solve)
    {
        
        foreach (var value in _table)
        {
            var newModel = new Model(model);
            newModel.Assign(literal.Value, value);

            var newFormula = new Formula(formula);
            var newLiteral = value ? literal : literal.Negative();
            
            _unitClause.Clear();
            _unitClause.AddLast(newLiteral);
            
            newFormula.Add(_unitClause);

            var resultModel = solve(newFormula, newModel);
            if (resultModel.IsSatisfied) return resultModel;
        }

        return model;
    }
}