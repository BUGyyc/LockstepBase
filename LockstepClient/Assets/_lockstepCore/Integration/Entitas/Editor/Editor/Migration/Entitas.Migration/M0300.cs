namespace Entitas.Migration;

public class M0300 : IMigration
{
	public string version => "0.30.0";

	public string workingDirectory => "project root";

	public string description => "Updates Entitas.properties to use renamed code generators";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path, "Entitas.properties");
		foreach (MigrationFile obj in files)
		{
			obj.fileContent = obj.fileContent.Replace("ComponentsGenerator", "ComponentExtensionsGenerator");
			obj.fileContent = obj.fileContent.Replace("PoolAttributeGenerator", "PoolAttributesGenerator");
		}
		return files;
	}
}
