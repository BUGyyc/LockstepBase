using System.Collections.Generic;
using System.Linq;

namespace Entitas.Migration;

public class M0410_3 : IMigration
{
	public string version => "0.41.0-3";

	public string workingDirectory => "where custom TypeDrawers are located";

	public string description => "Updating namespaces";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path);
		List<MigrationFile> list = new List<MigrationFile>();
		list.AddRange(updateNamespace(files, "Entitas.Unity.VisualDebugging", "Entitas.VisualDebugging.Unity.Editor"));
		return list.ToArray();
	}

	private MigrationFile[] updateNamespace(MigrationFile[] files, string oldNamespace, string newNamespace)
	{
		MigrationFile[] array = files.Where((MigrationFile f) => f.fileContent.Contains(oldNamespace)).ToArray();
		MigrationFile[] array2 = array;
		foreach (MigrationFile obj in array2)
		{
			obj.fileContent = obj.fileContent.Replace(oldNamespace, newNamespace);
		}
		return array;
	}
}
