using GaruffSolver.CNF;
using GaruffSolver.Test.Testcases;

namespace GaruffSolver.Test.DPLL;

public class DpllSolverTestWithTestCase
{
    private readonly CnfReader _reader = new();
    private readonly DpllSolver _solver = new();

    [TestCaseSource(typeof(TestcaseUtility), nameof(TestcaseUtility.Uf2091Cases))]
    public void ReadFileSolve_ReturnTrueTest(string fileName)
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("uf20-91", fileName);
        var cnf = _reader.ReadFromFile(cnfFilePath);
        var isSatisfiable = _solver.Solve(cnf);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.True);
    }

    [TestCaseSource(typeof(TestcaseUtility), nameof(TestcaseUtility.Uf502181000Cases))]
    public void ReadFileSolve_ReturnFalseTest(string fileName)
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("UUF50.218.1000", fileName);
        var cnf = _reader.ReadFromFile(cnfFilePath);
        var isSatisfiable = _solver.Solve(cnf);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.False);
    }
}