namespace Entitas.Migration;

public class M0360_1 : IMigration
{
	public string version => "0.36.0-1";

	public string workingDirectory => "project root";

	public string description => "Updates Entitas.properties to use renamed keys";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path, "Entitas.properties");
		foreach (MigrationFile obj in files)
		{
			obj.fileContent = obj.fileContent.Replace("Entitas.CodeGenerator.Pools", "Entitas.CodeGenerator.Contexts");
		}
		return files;
	}
}
