using GaruffSolver.CNF;
using GaruffSolver.Test.Testcases;

namespace GaruffSolver.Test.CNF;

public class CnfSimpleTest
{
    [Test]
    public void ReadFileTest()
    {
        var reader = new CnfReader();
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", "sample.cnf");
        var cnf = reader.ReadFromFile(cnfFilePath);

        Console.WriteLine(cnf);

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
    public void ReadFileSolve_ReturnTrueTest()
    {
        var reader = new CnfReader();
        var cnfFilePath = TestcaseUtility.GetTestcasePath("Sample", "sample.cnf");
        var cnf = reader.ReadFromFile(cnfFilePath);

        var solver = new DpllSolver();
        var isSatisfiable = solver.Solve(cnf);

        Console.WriteLine($"Is Satisfiable: {isSatisfiable}");

        Assert.That(isSatisfiable, Is.True);
    }
}