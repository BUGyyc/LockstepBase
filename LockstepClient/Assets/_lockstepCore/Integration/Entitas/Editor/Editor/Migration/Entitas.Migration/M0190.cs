using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Migration;

public class M0190 : IMigration
{
	private const string EXECUTE_PATTERN = "public\\s*void\\s*Execute\\s*\\(\\s*Entity\\s*\\[\\s*\\]\\s*entities\\s*\\)";

	private const string EXECUTE_REPLACEMENT = "public void Execute(System.Collections.Generic.List<Entity> entities)";

	public string version => "0.19.0";

	public string workingDirectory => "where all systems are located";

	public string description => "Migrates IReactiveSystem.Execute to accept List<Entity>";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] array = (from file in MigrationUtils.GetFiles(path)
			where Regex.IsMatch(file.fileContent, "public\\s*void\\s*Execute\\s*\\(\\s*Entity\\s*\\[\\s*\\]\\s*entities\\s*\\)")
			select file).ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].fileContent = Regex.Replace(array[i].fileContent, "public\\s*void\\s*Execute\\s*\\(\\s*Entity\\s*\\[\\s*\\]\\s*entities\\s*\\)", "public void Execute(System.Collections.Generic.List<Entity> entities)", RegexOptions.Multiline);
		}
		return array;
	}
}
