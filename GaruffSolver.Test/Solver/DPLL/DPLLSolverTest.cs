using GaruffSolver.Values;

namespace GaruffSolver.Test.DPLL;

public class DPLLSolverTest
{
    private readonly DpllSolver _solver = new();
    private readonly Literal A = Literal.Of("A");
    private readonly Literal B = Literal.Of("B");
    private readonly Literal C = Literal.Of("C");
    private readonly Literal D = Literal.Of("D");

    [Test]
    public void SatisfiableTest()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B }),
            new(new List<Literal> { A.Negative(), C }),
            new(new List<Literal> { B, C.Negative() })
        });

        var isSatisfiable = _solver.Solve(formula);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.True);
    }

    [Test]
    public void SameLiteralTest()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B }),
            new(new List<Literal> { A.Negative(), C }),
            new(new List<Literal> { C.Negative(), D }),
            new(new List<Literal> { A })
        });

        var isSatisfiable = _solver.Solve(formula);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.True);
    }

    [Test]
    public void DetectsUnsatisfiableFormulas()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A }),
            new(new List<Literal> { A.Negative() })
        });

        var isSatisfiable = _solver.Solve(formula);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.False);
    }

    [Test]
    public void UnsatisfiableFormulaTest()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B }),
            new(new List<Literal> { A.Negative(), B.Negative() }),
            new(new List<Literal> { A, B.Negative() }),
            new(new List<Literal> { A.Negative(), B })
        });

        var isSatisfiable = _solver.Solve(formula);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.False);
    }

    [Test]
    public void UnsatisfiableFormulaTest2()
    {
        var formula = new Formula(new List<Clause>
        {
            new(new List<Literal> { A, B }),
            new(new List<Literal> { A.Negative(), C }),
            new(new List<Literal> { B, C.Negative() })
        });

        var isSatisfiable = _solver.Solve(formula);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.True);
    }
}