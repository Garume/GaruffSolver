using System.Reflection;

namespace GaruffSolver.Test.Testcases;

public static class TestcaseUtility
{
    public static string GetTestcasePath(string folderName, string fileName)
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var directoryPath = Path.GetDirectoryName(assemblyLocation);

        // プロジェクトのルートディレクトリに戻る
        var projectRoot = Directory.GetParent(directoryPath).Parent.Parent.Parent.FullName;

        var resourcePath = $"GaruffSolver.Test/Testcases/{folderName}/{fileName}";
        var cnfFilePath = Path.Combine(projectRoot, resourcePath);

        return cnfFilePath;
    }

    public static string[] Uf2091Cases()
    {
        return Enumerable.Range(1, 10)
            .Select(i => $"uf20-0{i}.cnf")
            .ToArray();
    }

    public static string[] Uf502181000Cases()
    {
        return Enumerable.Range(1, 10)
            .Select(i => $"uuf50-0{i}.cnf")
            .ToArray();
    }
}