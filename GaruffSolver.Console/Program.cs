// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Solver.LiteralSelector;
using GaruffSolver.Test.Testcases;

internal class Program
{

    private static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<TestSat>();
    }
}

public class TestSat
{
    private readonly SolveBuilder _builder = new(new DpllUnitPropagator(), new DpllPureLiteralEliminator(),
        new MomsLiteralSelector(), new DpllBackTracker());

    private readonly CnfReader _reader = new();
    
    [Benchmark]
    public void Test()
    {
        var filePath = "C:\\Users\\Garume\\Documents\\Github\\GaruffSolver\\GaruffSolver.Test\\Testcases\\Sample\\sudoku.cnf";
        var cnf = _reader.ReadFromFile(filePath);
        var solver = new GaruffSolver.GaruffSolver(_builder);
        solver.Solve(cnf);
    }
}