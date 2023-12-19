using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Values;

namespace GaruffSolver.Test.Values.DPLL;

public class DpllUnitPropagationTest
{
    private readonly IUnitPropagator _propagator = new DpllUnitPropagator();

    private readonly Literal A = Literal.Of("A");
    private readonly Literal B = Literal.Of("B");
    private readonly Literal C = Literal.Of("C");
    private readonly Literal D = Literal.Of("D");
    private readonly Literal E = Literal.Of("E");

    [Test]
    public void UnitPropagation_SingleUnitClause_SetsLiteralTrue()
    {
        var formula = new Formula(new Clause[] { A });

        _propagator.UnitPropagation(ref formula, out var unitLiteral);

        Assert.That(unitLiteral, Is.EqualTo(A));
        Assert.That(formula, Is.EqualTo(new Formula(new Clause[] { A })));
    }

    [Test]
    public void UnitPropagation_ContradictoryUnitClauses_ReturnsFalse()
    {
        var formula = A & -A;

        _propagator.UnitPropagation(ref formula, out var unitLiteral);

        Assert.That(unitLiteral, Is.EqualTo(A));
        Assert.That(formula.First(), Is.EqualTo(new Clause(new[] { A })));
    }

    [Test]
    public void UnitPropagation_MultipleUnitClauses_SetsAllLiteralsTrue()
    {
        var formula = (A | B) & (-A | C) & (-C | D) & A;

        _propagator.UnitPropagation(ref formula, out var unitLiteral);

        var expected = C & (-C | D) & A;

        Assert.That(unitLiteral, Is.EqualTo(A));
        Assert.That(formula, Is.EqualTo(expected));
    }

    [Test]
    public void UnitPropagation_UnitClauseAffectingOtherClauses_SimplifiesFormula()
    {
        var formula = A & (A | B);

        _propagator.UnitPropagation(ref formula, out var unitLiteral);

        var expected = A;

        Assert.That(unitLiteral, Is.EqualTo(A));
        Assert.That(formula, Is.EqualTo(new Formula(new Clause[] { expected })));
    }

    [Test]
    public void UnitPropagation_NoUnitClauses_KeepsFormulaUnchanged()
    {
        var formula = (A | -B) & (C | -D);
        _propagator.UnitPropagation(ref formula, out var unitLiteral);

        Assert.That(unitLiteral, Is.Null);
        Assert.That(formula, Is.EqualTo(formula));
    }
}