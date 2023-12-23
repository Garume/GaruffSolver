using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Values;

namespace GaruffSolver.Test.DPLL;

public class DPLLSolverTest
{
    private readonly GaruffSolver _solver = new(new SolveBuilder(new DpllSolver()));
    private readonly Literal A = Literal.Of(1);
    private readonly Literal B = Literal.Of(2);
    private readonly Literal C = Literal.Of(3);
    private readonly Literal D = Literal.Of(4);

    [Test]
    public void SatisfiableTest()
    {
        var formula = (A | B) & (-A | C) & (B | -C);

        var model = _solver.Solve(formula);
        var verify = model.Verify(formula);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verify, Is.True);
    }

    [Test]
    public void SameLiteralTest()
    {
        var formula = (A | B) & (-A | C) & (-C | D) & A;

        var model = _solver.Solve(formula);
        var verify = model.Verify(formula);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verify, Is.True);
    }

    [Test]
    public void DetectsUnsatisfiableFormulas()
    {
        var formula = A & -A;

        var model = _solver.Solve(formula);

        Assert.That(model.IsSatisfied, Is.False);
    }

    [Test]
    public void UnsatisfiableFormulaTest()
    {
        var formula = (A | B) & (-A | -B) & (A | -B) & (-A | B);

        var model = _solver.Solve(formula);

        Assert.That(model.IsSatisfied, Is.False);
    }

    [Test]
    public void UnsatisfiableFormulaTest2()
    {
        var formula = (A | B) & (-A | C) & (B | -C);

        var model = _solver.Solve(formula);
        var verify = model.Verify(formula);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verify, Is.True);
    }
}