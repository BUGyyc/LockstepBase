using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Migration;

public class M0220 : IMigration
{
	private const string TRIGGER_PATTERN = "public\\s*IMatcher\\s*trigger\\s*\\{\\s*get\\s*\\{\\s*return\\s*(?<matcher>.*?)\\s*;\\s*\\}\\s*\\}";

	private const string EVENT_TYPE_PATTERN = "^\\s*public\\s*GroupEventType\\s*eventType\\s*\\{\\s*get\\s*\\{\\s*return\\s*GroupEventType\\.(?<eventType>\\w*)\\s*;\\s*\\}\\s*\\}";

	private const string TRIGGER_REPLACEMENT_FORMAT = "public TriggerOnEvent trigger {{ get {{ return {0}.{1}(); }} }}";

	public string version => "0.22.0";

	public string workingDirectory => "where all systems are located";

	public string description => "Migrates IReactiveSystem to combine trigger and eventTypes to TriggerOnEvent";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] array = (from file in MigrationUtils.GetFiles(path)
			where Regex.IsMatch(file.fileContent, "public\\s*IMatcher\\s*trigger\\s*\\{\\s*get\\s*\\{\\s*return\\s*(?<matcher>.*?)\\s*;\\s*\\}\\s*\\}")
			select file).ToArray();
		foreach (MigrationFile obj in array)
		{
			Match match2 = Regex.Match(obj.fileContent, "^\\s*public\\s*GroupEventType\\s*eventType\\s*\\{\\s*get\\s*\\{\\s*return\\s*GroupEventType\\.(?<eventType>\\w*)\\s*;\\s*\\}\\s*\\}", RegexOptions.Multiline);
			string eventType = match2.Groups["eventType"].Value;
			obj.fileContent = Regex.Replace(obj.fileContent, "^\\s*public\\s*GroupEventType\\s*eventType\\s*\\{\\s*get\\s*\\{\\s*return\\s*GroupEventType\\.(?<eventType>\\w*)\\s*;\\s*\\}\\s*\\}", string.Empty, RegexOptions.Multiline);
			obj.fileContent = Regex.Replace(obj.fileContent, "public\\s*IMatcher\\s*trigger\\s*\\{\\s*get\\s*\\{\\s*return\\s*(?<matcher>.*?)\\s*;\\s*\\}\\s*\\}", (Match match) => string.Format("public TriggerOnEvent trigger {{ get {{ return {0}.{1}(); }} }}", match.Groups["matcher"].Value, eventType), RegexOptions.Multiline);
		}
		return array;
	}
}
