using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Migration;

public class M0360_2 : IMigration
{
	public string version => "0.36.0-2";

	public string workingDirectory => "where systems are located";

	public string description => "Migrates systems";

	public MigrationFile[] Migrate(string path)
	{
		MigrationFile[] files = MigrationUtils.GetFiles(path);
		foreach (MigrationFile migrationFile in files)
		{
			migrationFile.fileContent = migrateBase(migrationFile.fileContent);
			migrationFile.fileContent = migrateTrigger(migrationFile.fileContent);
			migrationFile.fileContent = migrateToFilter(migrationFile.fileContent);
			migrationFile.fileContent = migrateSetPoolsSetPool(migrationFile.fileContent);
			migrationFile.fileContent = migrateExecute(migrationFile.fileContent);
		}
		return files;
	}

	private string migrateBase(string fileContent)
	{
		fileContent = removeBase(fileContent, "ISetPools");
		fileContent = removeBase(fileContent, "ISetPool");
		fileContent = removeBase(fileContent, "IEnsureComponents");
		fileContent = removeBase(fileContent, "IExcludeComponents");
		fileContent = renameBase(fileContent, "IReactiveSystem", "ReactiveSystem");
		return fileContent;
	}

	private string removeBase(string fileContent, string name)
	{
		fileContent = Regex.Replace(fileContent, "(,\\s*)" + name, string.Empty);
		fileContent = Regex.Replace(fileContent, "(:\\s*)" + name + "(,\\s*)", ": ");
		fileContent = Regex.Replace(fileContent, "(:\\s*)" + name, string.Empty);
		return fileContent;
	}

	private string renameBase(string fileContent, string name, string replacement)
	{
		fileContent = Regex.Replace(fileContent, "(,\\s*)" + name, ", " + replacement);
		fileContent = Regex.Replace(fileContent, "(:\\s*)" + name + "(,\\s*)", ": " + replacement);
		fileContent = Regex.Replace(fileContent, "(:\\s*)" + name, ": " + replacement);
		return fileContent;
	}

	private string migrateTrigger(string fileContent)
	{
		string value = Regex.Match(fileContent, "public(\\s|\\n)*TriggerOnEvent(\\s|\\n)*trigger(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<trigger>(.|\\s|\\n)*?})(\\s|\\n)*}").Groups["trigger"].Value;
		string groupEvent = Regex.Match(value, ".OnEntity(?<event>\\w*)").Groups["event"].Value;
		Match oldMatcher = Regex.Match(value, ".*(?=.OnEntity)");
		fileContent = ((!(groupEvent == "Added")) ? Regex.Replace(fileContent, "public(\\s|\\n)*TriggerOnEvent(\\s|\\n)*trigger(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<trigger>(.|\\s|\\n)*?})(\\s|\\n)*}", (Match match) => $"__ctor_placeholder__\n\n    protected override Collector GetTrigger(Context context) {{\n        return context.CreateCollector({oldMatcher}, GroupEvent.{groupEvent});\n    }}\n\n__filter_placeholder__") : Regex.Replace(fileContent, "public(\\s|\\n)*TriggerOnEvent(\\s|\\n)*trigger(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<trigger>(.|\\s|\\n)*?})(\\s|\\n)*}", (Match match) => $"__ctor_placeholder__\n\n    protected override Collector GetTrigger(Context context) {{\n        return context.CreateCollector({oldMatcher});\n    }}\n\n__filter_placeholder__"));
		return fileContent;
	}

	private string migrateToFilter(string fileContent)
	{
		string value = Regex.Match(fileContent, "public(\\s|\\n)*IMatcher(\\s|\\n)*ensureComponents(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<matcher>(.|\\s|\\n)*?);(\\s|\\n)*}(\\s|\\n)*}").Groups["matcher"].Value;
		string value2 = Regex.Match(fileContent, "public(\\s|\\n)*IMatcher(\\s|\\n)*excludeComponents(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<matcher>(.|\\s|\\n)*?);(\\s|\\n)*}(\\s|\\n)*}").Groups["matcher"].Value;
		string text = getFilter(value);
		if (!string.IsNullOrEmpty(text))
		{
			text = "(" + text + ")";
		}
		string text2 = getFilter(value2);
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = "!(" + text2 + ")";
		}
		string text3 = "true";
		if (text != null)
		{
			text3 = text;
		}
		if (text2 != null)
		{
			text3 = ((text != null) ? (text3 + " && " + text2) : text2);
		}
		fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*IMatcher(\\s|\\n)*ensureComponents(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<matcher>(.|\\s|\\n)*?);(\\s|\\n)*}(\\s|\\n)*}", string.Empty);
		fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*IMatcher(\\s|\\n)*excludeComponents(\\s|\\n)*{(\\s|\\n)*get(\\s|\\n)*{(\\s|\\n)*return(\\s|\\n)*(?<matcher>(.|\\s|\\n)*?);(\\s|\\n)*}(\\s|\\n)*}", string.Empty);
		fileContent = Regex.Replace(fileContent, "__filter_placeholder__", $"    protected override bool Filter(Entity entity) {{\n        // TODO Entitas 0.36.0 Migration\n        // ensure was: {value}\n        // exclude was: {value2}\n\n        return {text3};\n    }}");
		return fileContent;
	}

	private string getFilter(string matcher)
	{
		string text = string.Empty;
		if (Regex.IsMatch(matcher, "AllOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)"))
		{
			text = string.Join(" && ", (from m in Regex.Match(matcher, "AllOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)").Groups["matchers"].Value.Split(',')
				select m.Trim() into m
				select m.Split('.')[1] into m
				select "entity.has" + m).ToArray());
		}
		string text2 = string.Empty;
		if (Regex.IsMatch(matcher, "AnyOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)"))
		{
			text2 = string.Join(" || ", (from m in Regex.Match(matcher, "AnyOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)").Groups["matchers"].Value.Split(',')
				select m.Trim() into m
				select m.Split('.')[1] into m
				select "entity.has" + m).ToArray());
		}
		string text3 = string.Empty;
		if (Regex.IsMatch(matcher, "NoneOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)"))
		{
			text3 = string.Join(" && !", (from m in Regex.Match(matcher, "NoneOf(\\s|\\n)*\\((\\s|\\n)*(?<matchers>(.|\\s|\\n)*?)\\)").Groups["matchers"].Value.Split(',')
				select m.Trim() into m
				select m.Split('.')[1] into m
				select "entity.has" + m).ToArray());
		}
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(text))
		{
			text = "(" + text + ")";
			list.Add(text);
		}
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = "(" + text2 + ")";
			list.Add(text2);
		}
		if (!string.IsNullOrEmpty(text3))
		{
			text3 = "(!" + text3 + ")";
			list.Add(text3);
		}
		if (list.Count == 0 && Regex.IsMatch(matcher, "\\w*Matcher.\\w*"))
		{
			list.Add(matcher.Split('.')[1]);
		}
		if (list.Count != 0)
		{
			return string.Join(" && ", list.ToArray());
		}
		return null;
	}

	private string migrateSetPoolsSetPool(string fileContent)
	{
		string value = Regex.Match(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPools(\\s|\\n)*\\((\\s|\\n)*Contexts(\\s|\\n)*pools(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}").Groups["logic"].Value;
		Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPools(\\s|\\n)*\\((\\s|\\n)*Contexts(\\s|\\n)*pools(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", string.Empty);
		string value2 = Regex.Match(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPool(\\s|\\n)*\\((\\s|\\n)*Context(\\s|\\n)*pool(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}").Groups["logic"].Value;
		Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPool(\\s|\\n)*\\((\\s|\\n)*Context(\\s|\\n)*pool(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", string.Empty);
		string pattern = "public(\\w|\\s|\\n)*class(\\s|\\n)(?<className>\\w*)";
		string value3 = Regex.Match(fileContent, pattern).Groups["className"].Value;
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(value))
		{
			list.Add("        " + value);
		}
		if (!string.IsNullOrEmpty(value2))
		{
			list.Add("        " + value2);
		}
		if (fileContent.Contains("__ctor_placeholder__"))
		{
			fileContent = fileContent.Replace("__ctor_placeholder__", string.Format("public {0}(Contexts contexts) : base(context) {{\n{1}\n    }}", value3, string.Join("    \n", list.ToArray())));
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPools(\\s|\\n)*\\((\\s|\\n)*Contexts(\\s|\\n)*pools(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", string.Empty);
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPool(\\s|\\n)*\\((\\s|\\n)*Context(\\s|\\n)*pool(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", string.Empty);
		}
		else
		{
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPools(\\s|\\n)*\\((\\s|\\n)*Contexts(\\s|\\n)*pools(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", (Match match) => "// TODO Entitas 0.36.0 Migration (constructor)\n    " + match.Value);
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*SetPool(\\s|\\n)*\\((\\s|\\n)*Context(\\s|\\n)*pool(\\s|\\n)*\\)(\\s|\\n)*{(\\s|\\n)*(?<logic>(.|\\s|\\n)*?)(\\s|\\n)*}", (Match match) => "// TODO Entitas 0.36.0 Migration (constructor)\n    " + match.Value);
		}
		return fileContent;
	}

	private string migrateExecute(string fileContent)
	{
		if (Regex.IsMatch(fileContent, "public(\\s|\\n)*void(\\s|\\n)*Execute(\\s|\\n)*\\((\\s|\\n)*System.Collections.Generic.List"))
		{
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*Execute(\\s|\\n)*\\((\\s|\\n)*List", "protected override void Execute(System.Collections.Generic.List");
		}
		else if (Regex.IsMatch(fileContent, "public(\\s|\\n)*void(\\s|\\n)*Execute(\\s|\\n)*\\((\\s|\\n)*List"))
		{
			fileContent = Regex.Replace(fileContent, "public(\\s|\\n)*void(\\s|\\n)*Execute(\\s|\\n)*\\((\\s|\\n)*List", "protected override void Execute(List");
		}
		return fileContent;
	}
}
