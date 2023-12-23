// See https://aka.ms/new-console-template for more information


using GaruffSolver.CNF;
using GaruffSolver.Solver;
using GaruffSolver.Solver.DPLL;
using GaruffSolver.Solver.LiteralSelector;
using GaruffSolver.Test.Testcases;

var filePath = TestcaseUtility.GetTestcasePath("Sample", "sudoku.cnf");
var cnf = new CnfReader().ReadFromFile(filePath);
var builder = new SolveBuilder(new DpllUnitPropagator(), new DpllPureLiteralEliminator(),
    new MomsLiteralSelector(), new DpllBackTracker());
var solver = new GaruffSolver.GaruffSolver(builder);
var model = solver.Solve(cnf);