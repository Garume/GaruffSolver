using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Values;

namespace GaruffSolver.Test.Values.DPLL;

public class DpllPureLiteralEliminationTest
{
    private readonly IPureLiteralEliminator _eliminator = new DpllPureLiteralEliminator();

    private readonly Literal A = Literal.Of("A");
    private readonly Literal B = Literal.Of("B");
    private readonly Literal C = Literal.Of("C");
    private readonly Literal D = Literal.Of("D");
    private readonly Literal E = Literal.Of("E");

    [Test]
    public void PureLiteralElimination_RemovesClausesWithPureLiterals()
    {
        var formula = (A | B) & (-B | C) & (-C | A);

        _eliminator.PureLiteralElimination(ref formula, out var pureLiterals);

        var expected = -B | C;

        Assert.That(pureLiterals, Is.EqualTo(new List<Literal> { A }));
        Assert.That(formula, Is.EqualTo(new Formula(new[] { expected })));
    }

    [Test]
    public void PureLiteralElimination_WithNoPureLiterals_KeepsFormulaUnchanged()
    {
        var formula = (A | -B) & (-A | C) & (B | -C);

        _eliminator.PureLiteralElimination(ref formula, out var pureLiterals);

        Assert.That(pureLiterals, Is.Empty);
        Assert.That(formula, Is.EqualTo(formula));
    }

    [Test]
    public void PureLiteralElimination_WithMultiplePureLiterals_RemovesMultipleClauses()
    {
        var formula = A & B & (-C | D) & (-D | E);

        _eliminator.PureLiteralElimination(ref formula, out var pureLiterals);

        Assert.That(pureLiterals, Is.EqualTo(new List<Literal> { A, B, -C, E }));
        Assert.That(formula, Is.Empty);
    }

    [Test]
    public void PureLiteralElimination_WithAllClausesContainingPureLiterals_RemovesAllClauses()
    {
        var formula = A & -B;

        _eliminator.PureLiteralElimination(ref formula, out var pureLiterals);

        Assert.That(pureLiterals, Is.EqualTo(new List<Literal> { A, -B }));
        Assert.That(formula, Is.Empty);
    }
}