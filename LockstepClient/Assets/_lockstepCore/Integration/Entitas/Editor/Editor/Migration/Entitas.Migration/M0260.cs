using System.Linq;

namespace Entitas.Migration;

public class M0260 : IMigration
{
	private const string POOL_PATTERN_1 = "var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(";

	private const string POOL_PATTERN_2 = "UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);";

	private const string COMPONENT_PATTERN = "throw new SingleEntityException(";

	private const string REPLACEMENT = "//";

	public string version => "0.26.0";

	public string workingDirectory => "where generated files are located";

	public string description => "Deactivates code to prevent compile erros";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] array = (from file in MigrationUtils.GetFiles(path)
			where file.fileContent.Contains("var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(") || file.fileContent.Contains("throw new SingleEntityException(")
			select file).ToArray();
		foreach (MigrationFile obj in array)
		{
			obj.fileContent = obj.fileContent.Replace("var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(", "//var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(");
			obj.fileContent = obj.fileContent.Replace("UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);", "//UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);");
			obj.fileContent = obj.fileContent.Replace("throw new SingleEntityException(", "//throw new SingleEntityException(");
		}
		return array;
	}
}
