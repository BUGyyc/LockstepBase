namespace Entitas.Migration;

public class M0472 : IMigration
{
	public string version => "0.47.2";

	public string workingDirectory => "project root";

	public string description => "Updates properties to use renamed keys";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path, "*.properties");
		foreach (MigrationFile obj in files)
		{
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.SearchPaths", "Jenny.SearchPaths");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.Plugins", "Jenny.Plugins");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.PreProcessors", "Jenny.PreProcessors");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.DataProviders", "Jenny.DataProviders");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.CodeGenerators", "Jenny.CodeGenerators");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.PostProcessors", "Jenny.PostProcessors");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.CLI.Ignore.UnusedKeys", "Jenny.Ignore.Keys");
			obj.fileContent = obj.fileContent.Replace("Ignore.Keys", "Jenny.Ignore.Keys");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.Server.Port", "Jenny.Server.Port");
			obj.fileContent = obj.fileContent.Replace("CodeGenerator.Client.Host", "Jenny.Client.Host");
		}
		return files;
	}
}
