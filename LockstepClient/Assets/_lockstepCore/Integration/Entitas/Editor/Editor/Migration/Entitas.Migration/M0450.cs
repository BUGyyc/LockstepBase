namespace Entitas.Migration;

public class M0450 : IMigration
{
	public string version => "0.45.0";

	public string workingDirectory => "project root";

	public string description => "Updates Entitas.properties to use renamed keys";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path, "Entitas.properties");
		foreach (MigrationFile obj in files)
		{
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.SearchPaths", "CodeGenerator.SearchPaths");
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.Plugins", "CodeGenerator.Plugins");
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.DataProviders", "CodeGenerator.DataProviders");
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.CodeGenerators", "CodeGenerator.CodeGenerators");
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.PostProcessors", "CodeGenerator.PostProcessors");
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGeneration.CodeGenerator.CLI.Ignore.UnusedKeys", "CodeGenerator.CLI.Ignore.UnusedKeys");
		}
		return files;
	}
}
