/*using GaruffSolver.CNF;
using GaruffSolver.Values;

namespace GaruffSolver;

public class DpllSolver
{
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
        Formula previous, result = new(formula);
        do
        {
            previous = result;
            result = previous.UnitPropagation(out var unitLiteral).PureLiteralElimination(out var pureLiterals);

            if (unitLiteral != null) model.Assign(unitLiteral.Name, unitLiteral.IsPositive);
            foreach (var pureLiteral in pureLiterals) model.Assign(pureLiteral.Name, pureLiteral.IsPositive);
        } while (!result.Equals(previous));

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

        var variable = SelectUnassignedVariable(result, model);

        foreach (var value in new[] { true, false })
        {
            var newModel = new Model(model);
            newModel.Assign(variable.Name, value);
            var newFormula = AssignValue(result, variable, value);

            var resultModel = Solve(newFormula, newModel);
            if (resultModel.IsSatisfied) return resultModel;
        }

        return model;
    }

    private Literal SelectUnassignedVariable(Formula formula, Model model)
    {
        return formula.SelectMany(clause => clause).Distinct()
            .First(literal => !model.TryGetValue(literal.Name, out _));
    }

    private Formula AssignValue(Formula formula, Literal literal, bool value)
    {
        return new Formula(formula) { new(new List<Literal> { new(literal.Name, value) }) };
    }
}*/

