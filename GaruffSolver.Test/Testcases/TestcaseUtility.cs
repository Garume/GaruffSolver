using System.Reflection;

namespace GaruffSolver.Test.Testcases;

public static class TestcaseUtility
{
    public static string GetTestcasePath(string folderName, string fileName)
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var directoryPath = Path.GetDirectoryName(assemblyLocation);

        var projectRoot = Directory.GetParent(directoryPath).Parent.Parent.Parent.FullName;

        var resourcePath = $"GaruffSolver.Test/Testcases/{folderName}/{fileName}";
        var cnfFilePath = Path.Combine(projectRoot, resourcePath);

        return cnfFilePath;
    }

    public static string[] Uf2091Cases()
    {
        return Enumerable.Range(1, 10)
            .Select(i => $"uf20-0{i}.cnf")
            .Select(i => GetTestcasePath("uf20-91", i))
            .ToArray();
    }

    public static string[] Uf502181000Cases()
    {
        return Enumerable.Range(1, 10)
            .Select(i => $"uuf50-0{i}.cnf")
            .Select(i => GetTestcasePath("UUF50.218.1000", i))
            .ToArray();
    }

    public static string[] Uf100430Cases()
    {
        return Enumerable.Range(1, 10)
            .Select(i => $"uf100-0{i}.cnf")
            .Select(i => GetTestcasePath("uf100-430", i))
            .ToArray();
    }
}