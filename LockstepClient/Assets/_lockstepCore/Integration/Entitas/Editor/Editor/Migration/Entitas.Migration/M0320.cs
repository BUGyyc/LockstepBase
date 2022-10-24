using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Migration;

public class M0320 : IMigration
{
	public string version => "0.32.0";

	public string workingDirectory => "project root";

	public string description => "Updates Entitas.properties to use renamed keys and updates calls to pool.CreateSystem<T>()";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path, "Entitas.properties");
		foreach (MigrationFile obj in files)
		{
			obj.fileContent = obj.fileContent.Replace("Entitas.Unity.CodeGenerator.GeneratedFolderPath", "Entitas.CodeGenerator.GeneratedFolderPath");
			obj.fileContent = obj.fileContent.Replace("Entitas.Unity.CodeGenerator.Pools", "Entitas.CodeGenerator.Pools");
			obj.fileContent = obj.fileContent.Replace("Entitas.Unity.CodeGenerator.EnabledCodeGenerators", "Entitas.CodeGenerator.EnabledCodeGenerators");
		}
		MigrationFile[] array = (from file in MigrationUtils.GetFiles(path)
			where Regex.IsMatch(file.fileContent, ".CreateSystem<(?<system>\\w*)>\\(\\s*\\)")
			select file).ToArray();
		foreach (MigrationFile obj2 in array)
		{
			obj2.fileContent = Regex.Replace(obj2.fileContent, ".CreateSystem<(?<system>\\w*)>\\(\\s*\\)", (Match match) => ".CreateSystem(new " + match.Groups["system"].Value + "())");
		}
		return files.Concat(array).ToArray();
	}
}
