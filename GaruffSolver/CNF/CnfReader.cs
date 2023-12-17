namespace GaruffSolver.CNF;

public class CnfReader
{
    public Cnf ReadFromFile(string filePath)
    {
        using var reader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

        var variableCount = 0;
        var clausesCount = 0;
        var clauses = new List<Dictionary<string, bool>>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null) continue;
            if (line.StartsWith("c")) continue;
            if (line.StartsWith("p"))
            {
                var split = line.Split(" ").Where(l => !string.IsNullOrEmpty(l)).ToArray();
                variableCount = int.Parse(split[2]);
                clausesCount = int.Parse(split[3]);
                continue;
            }

            if (line.StartsWith("%")) break;

            var splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var clause = new Dictionary<string, bool>();
            foreach (var literal in splitLine)
            {
                if (literal == "0") continue;
                var isPositive = literal.StartsWith("-");
                var name = isPositive ? literal[1..] : literal;
                clause.Add(name, !isPositive);
            }

            clauses.Add(clause);
        }

        return new Cnf(variableCount, clausesCount, clauses);
    }
}