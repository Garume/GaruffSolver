using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Test.Testcases;

namespace GaruffSolver.Test.DPLL;

public class DpllSolverTestWithTestCase
{
    private readonly CnfReader _reader = new();
    private readonly GaruffSolver _solver = new(new SolveBuilder(new DpllSolver()));

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
}