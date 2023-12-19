using GaruffSolver.Values;

namespace GaruffSolver.Solver.LiteralSelector;

public class MomsLiteralSelector : ILiteralSelector
{
    public LiteralSelectorFactory LiteralSelect => SelectLiteral;

    private Literal SelectLiteral(Formula formula, Model model)
    {
        // 最小サイズの節を見つける
        var minSize = formula.Min(clause => clause.Count);
        var smallestClauses = formula.Where(clause => clause.Count == minSize)
            .SelectMany(clause => clause)
            .GroupBy(literal => literal)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Select(group => group.Key)
            .First();

        return smallestClauses;
    }
}