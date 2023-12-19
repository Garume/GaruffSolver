using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Solver.LiteralSelector;
using GaruffSolver.Test.Testcases;

namespace GaruffSolver.Test.Custom;

public class CustomTestWithTestCase
{
    private readonly CnfReader _reader = new();
    private GaruffSolver _solver;

    [SetUp]
    public void SetUp()
    {
        var builder = new SolveBuilder(new DpllUnitPropagator(), new DpllPureLiteralEliminator(),
            new MomsLiteralSelector(), new DpllBackTracker());

        _solver = new GaruffSolver(builder);
    }

    [TestCaseSource(typeof(TestcaseUtility), nameof(TestcaseUtility.Uf2091Cases))]
    public void ReadFileSolve_ReturnTrueTest(string filePath)
    {
        var cnf = _reader.ReadFromFile(filePath);
        var model = _solver.Solve(cnf);
        var verify = model.Verify(cnf);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verify, Is.True);
    }

    [TestCaseSource(typeof(TestcaseUtility), nameof(TestcaseUtility.Uf502181000Cases))]
    public void ReadFileSolve_ReturnFalseTest(string filePath)
    {
        var cnf = _reader.ReadFromFile(filePath);
        var model = _solver.Solve(cnf);

        Assert.That(model.IsSatisfied, Is.False);
    }

    [TestCase("sudoku.cnf")]
    public void Sudoku_ReturnTrueTest(string fileName)
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", fileName);
        var cnf = _reader.ReadFromFile(cnfFilePath);

        var model = _solver.Solve(cnf);
        var verifyModel = model.Verify(cnf);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verifyModel, Is.True);
    }
}