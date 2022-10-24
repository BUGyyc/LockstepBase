using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Migration;

public class M0180 : IMigration
{
	private const string METHOD_END_PATTERN = "(\\s|.)*?\\}";

	private const string TRIGGER_PATTERN = "public\\s*IMatcher\\s*GetTriggeringMatcher\\s*\\(\\s*\\)\\s*\\{\\s*";

	private const string TRIGGER_END_PATTERN = "public\\s*IMatcher\\s*GetTriggeringMatcher\\s*\\(\\s*\\)\\s*\\{\\s*(\\s|.)*?\\}";

	private const string TRIGGER_REPLACEMENT = "public IMatcher trigger { get { ";

	private const string EVENT_TYPE_PATTERN = "public\\s*GroupEventType\\s*GetEventType\\s*\\(\\s*\\)\\s*\\{\\s*";

	private const string EVENT_TYPE_PATTERN_END = "public\\s*GroupEventType\\s*GetEventType\\s*\\(\\s*\\)\\s*\\{\\s*(\\s|.)*?\\}";

	private const string EVENT_TYPE_REPLACEMENT = "public GroupEventType eventType { get { ";

	public string version => "0.18.0";

	public string workingDirectory => "where all systems are located";

	public string description => "Migrates IReactiveSystem GetXyz methods to getters";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] array = (from file in MigrationUtils.GetFiles(path)
			where Regex.IsMatch(file.fileContent, "public\\s*IMatcher\\s*GetTriggeringMatcher\\s*\\(\\s*\\)\\s*\\{\\s*") || Regex.IsMatch(file.fileContent, "public\\s*GroupEventType\\s*GetEventType\\s*\\(\\s*\\)\\s*\\{\\s*")
			select file).ToArray();
		foreach (MigrationFile obj in array)
		{
			obj.fileContent = Regex.Replace(obj.fileContent, "public\\s*IMatcher\\s*GetTriggeringMatcher\\s*\\(\\s*\\)\\s*\\{\\s*(\\s|.)*?\\}", (Match match) => match.Value + " }", RegexOptions.Multiline);
			obj.fileContent = Regex.Replace(obj.fileContent, "public\\s*GroupEventType\\s*GetEventType\\s*\\(\\s*\\)\\s*\\{\\s*(\\s|.)*?\\}", (Match match) => match.Value + " }", RegexOptions.Multiline);
			obj.fileContent = Regex.Replace(obj.fileContent, "public\\s*IMatcher\\s*GetTriggeringMatcher\\s*\\(\\s*\\)\\s*\\{\\s*", "public IMatcher trigger { get { ", RegexOptions.Multiline);
			obj.fileContent = Regex.Replace(obj.fileContent, "public\\s*GroupEventType\\s*GetEventType\\s*\\(\\s*\\)\\s*\\{\\s*", "public GroupEventType eventType { get { ", RegexOptions.Multiline);
		}
		return array;
	}
}
