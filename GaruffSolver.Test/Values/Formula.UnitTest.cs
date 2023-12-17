using GaruffSolver.Values;

namespace GaruffSolver.Test.Values;

public class FormulaUnitTest
{
    private readonly Literal A = Literal.Of("A");
    private readonly Literal B = Literal.Of("B");
    private readonly Literal C = Literal.Of("C");
    private readonly Literal D = Literal.Of("D");
    private readonly Literal E = Literal.Of("E");

    [Test]
    public void UnitPropagation_SingleUnitClause_SetsLiteralTrue()
    {
        var formula = new Formula(new List<Clause> { new(new[] { A }) });

        Console.WriteLine($"Formula: {formula}");
        var ans = formula.UnitPropagation();

        Console.WriteLine($"UnitPropagation: {ans}");

        Assert.That(ans, Is.EqualTo(formula));
    }

    [Test]
    public void UnitPropagation_ContradictoryUnitClauses_ReturnsFalse()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new[] { A }),
            new(new[] { A.Negative() })
        });

        Console.WriteLine($"Formula: {formula}");
        var ans = formula.UnitPropagation();

        Console.WriteLine(ans);


        Assert.That(ans.First(), Is.EqualTo(new Clause(new[] { A })));
    }

    [Test]
    public void UnitPropagation_MultipleUnitClauses_SetsAllLiteralsTrue()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new[] { A, B }),
            new(new[] { A.Negative(), C }),
            new(new[] { C.Negative(), D }),
            new(new[] { A })
        });

        Console.WriteLine($"Formula: {formula}");
        var ans = formula.UnitPropagation();
        Console.WriteLine($"UnitPropagation: {ans}");

        var expected = new Formula(new List<Clause>
        {
            new(new[] { C }),
            new(new[] { C.Negative(), D }),
            new(new[] { A })
        });

        Assert.That(ans, Is.EqualTo(expected));
    }

    [Test]
    public void UnitPropagation_UnitClauseAffectingOtherClauses_SimplifiesFormula()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new[] { A }),
            new(new[] { A, B })
        });
        Console.WriteLine($"Formula: {formula}");
        var ans = formula.UnitPropagation();
        Console.WriteLine($"UnitPropagation: {ans}");

        var expected = new Formula(new List<Clause>
        {
            new(new[] { A })
        });

        Assert.That(ans, Is.EqualTo(expected));
    }

    [Test]
    public void UnitPropagation_NoUnitClauses_KeepsFormulaUnchanged()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B.Negative() }),
            new(new List<Literal> { C, D.Negative() })
        });
        var ans = formula.UnitPropagation();

        Assert.That(ans, Has.Count.EqualTo(2));
    }


    [Test]
    public void PureLiteralElimination_RemovesClausesWithPureLiterals()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new[] { A, B }),
            new(new[] { B.Negative(), C }),
            new(new[] { C.Negative(), A })
        });

        Console.WriteLine($"Formula before PureLiteralElimination: {formula}");
        var ans = formula.PureLiteralElimination();
        Console.WriteLine($"Formula after PureLiteralElimination: {ans}");

        var expected = new Formula(new List<Clause>
        {
            new(new List<Literal> { B.Negative(), C })
        });

        Console.WriteLine($"Expected: {expected}");

        Assert.That(ans, Is.EqualTo(expected));
    }

    [Test]
    public void PureLiteralElimination_WithNoPureLiterals_KeepsFormulaUnchanged()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B.Negative() }),
            new(new List<Literal> { A.Negative(), C }),
            new(new List<Literal> { B, C.Negative() })
        });

        Console.WriteLine($"Formula before PureLiteralElimination: {formula}");
        var ans = formula.PureLiteralElimination();
        Console.WriteLine($"Formula after PureLiteralElimination: {ans}");

        var expected = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B.Negative() }),
            new(new List<Literal> { A.Negative(), C }),
            new(new List<Literal> { B, C.Negative() })
        });

        Console.WriteLine($"Expected: {expected}");

        Assert.That(ans, Is.EqualTo(expected));
    }

    [Test]
    public void PureLiteralElimination_WithMultiplePureLiterals_RemovesMultipleClauses()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A }),
            new(new List<Literal> { B }),
            new(new List<Literal> { C.Negative(), D }),
            new(new List<Literal> { D.Negative(), E })
        });

        Console.WriteLine($"Formula before PureLiteralElimination: {formula}");
        var ans = formula.PureLiteralElimination();
        Console.WriteLine($"Formula after PureLiteralElimination: {ans}");

        var expected = new Formula(new List<Clause>());

        Console.WriteLine($"Expected: {expected}");

        Assert.That(ans, Is.Empty);
    }

    [Test]
    public void PureLiteralElimination_WithAllClausesContainingPureLiterals_RemovesAllClauses()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A }),
            new(new List<Literal> { B.Negative() })
        });

        Console.WriteLine($"Formula before PureLiteralElimination: {formula}");
        var ans = formula.PureLiteralElimination();
        Console.WriteLine($"Formula after PureLiteralElimination: {ans}");

        var expected = new Formula(new List<Clause>());

        Console.WriteLine($"Expected: {expected}");

        Assert.That(ans, Is.Empty);
    }
}