using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Values;

namespace GaruffSolver;

public class GaruffSolver
{
    private readonly SolveBuilder _builder;

    public GaruffSolver(SolveBuilder builder)
    {
        _builder = builder;
    }

    public Model Solve(Cnf cnf)
    {
        var formula = new Formula(cnf);
        var model = Solve(formula, new Model());
        model.Fill(cnf.GetVariables());
        return model;
    }

    public Model Solve(Formula formula)
    {
        var model = Solve(formula, new Model());
        model.Fill(formula.GetVariables());
        return model;
    }


    private Model Solve(Formula formula, Model model)
    {
        var result = Simplify(formula, model);

        // 終了条件の確認
        if (result.Count == 0) // 全ての節が除去された場合、充足可能
        {
            model.IsSatisfied = true;
            return model;
        }

        if (result.Any(clause => clause.IsEmpty)) // 空の節がある場合、充足不可能
        {
            model.IsSatisfied = false;
            return model;
        }

        var variable = _builder.LiteralSelect(result, model);
        var newModel = _builder.BackTrack(result, variable, model, Solve);
        return newModel;
    }

    private Formula Simplify(Formula formula, Model model)
    {
        Formula result = new(formula);
        var isModified = false;
        do
        {
            isModified = false;

            _builder.UnitPropagation(ref result, out var unitLiteral);
            _builder.PureLiteralElimination(ref result, out var pureLiterals);

            if (unitLiteral != null)
            {
                model.Assign(unitLiteral.Name, unitLiteral.IsPositive);
                isModified = true;
            }

            foreach (var pureLiteral in pureLiterals)
            {
                model.Assign(pureLiteral.Name, pureLiteral.IsPositive);
                isModified = true;
            }
        } while (isModified);

        return result;
    }
}