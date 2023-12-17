using GaruffSolver.CNF;
using GaruffSolver.Values;

namespace GaruffSolver;

public class DpllSolver
{
    public bool Solve(Cnf cnf)
    {
        var formula = new Formula(cnf);
        return Solve(formula);
    }

    public bool Solve(Formula formula)
    {
        Console.WriteLine($"Formula: {formula}");


        Formula previous, result = new(formula);
        do
        {
            previous = result;
            result = previous.UnitPropagation().PureLiteralElimination();
        } while (!result.Equals(previous));


        Console.WriteLine($"ConvertedFormula: {result}");

        // 終了条件の確認
        if (result.Count == 0) // 全ての節が除去された場合、充足可能
            return true;
        if (result.Any(clause => clause.IsEmpty)) // 空の節がある場合、充足不可能
            return false;

        var variable = SelectUnassignedVariable(result);

        foreach (var value in new[] { true, false })
        {
            var newFormula = AssignValue(result, variable, value);
            if (Solve(newFormula))
                return true;
        }

        return false;
    }

    private Literal SelectUnassignedVariable(Formula formula)
    {
        return formula.SelectMany(clause => clause).Distinct().First();
    }

    private Formula AssignValue(Formula formula, Literal literal, bool value)
    {
        return new Formula(formula) { new(new List<Literal> { new(literal.Name, value) }) };
    }

    public bool IsConsistent(Formula formula)
    {
        if (formula.Any(clause => !clause.IsUnit)) return false;

        var literals = formula.SelectMany(c => c).Distinct().ToList();

        return literals.All(literal => literal.IsPure(literals));
    }
}