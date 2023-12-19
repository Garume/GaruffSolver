using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Test.Testcases;

namespace GaruffSolver.Test.CNF;

public class CnfSimpleTest
{
    private readonly CnfReader _reader = new();

    [Test]
    public void ReadFileTest()
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", "sample.cnf");
        var cnf = _reader.ReadFromFile(cnfFilePath);

        Assert.That(cnf.VariableCount, Is.EqualTo(5));
        Assert.That(cnf.ClausesCount, Is.EqualTo(3));
        Assert.That(cnf.Clauses.Count(), Is.EqualTo(3));

        var clauses = cnf.Clauses.ToList();
        Assert.That(clauses[0],
            Is.EqualTo(new Dictionary<string, bool> { { "1", true }, { "5", false }, { "4", true } }));
        Assert.That(clauses[1],
            Is.EqualTo(new Dictionary<string, bool> { { "1", false }, { "5", true }, { "3", true }, { "4", true } }));
        Assert.That(clauses[2], Is.EqualTo(new Dictionary<string, bool> { { "3", false }, { "4", false } }));
    }

    [Test]
    public void ReadFileSolve_ReturnFalseTest()
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", "sudoku.cnf");
        var cnf = _reader.ReadFromFile(cnfFilePath);

        Assert.That(cnf.VariableCount, Is.EqualTo(729));
    }

    [TestCase("sample.cnf")]
    [TestCase("binary.cnf")]
    public void ReadFileSolve_ReturnTrueTest(string fileName)
    {
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", fileName);
        var cnf = _reader.ReadFromFile(cnfFilePath);

        var solver = new GaruffSolver(new SolveBuilder(new DpllSolver()));
        var model = solver.Solve(cnf);
        var verifyModel = model.Verify(cnf);

        Assert.That(model.IsSatisfied, Is.True);
        Assert.That(verifyModel, Is.True);
    }
}