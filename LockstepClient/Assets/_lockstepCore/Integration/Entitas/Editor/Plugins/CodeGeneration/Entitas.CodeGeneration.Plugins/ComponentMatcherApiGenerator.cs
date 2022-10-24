using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentMatcherApiGenerator : AbstractGenerator
{
	private const string TEMPLATE = "public sealed partial class ${MatcherType} {\n\n    static Entitas.IMatcher<${EntityType}> _matcher${ComponentName};\n\n    public static Entitas.IMatcher<${EntityType}> ${ComponentName} {\n        get {\n            if (_matcher${ComponentName} == null) {\n                var matcher = (Entitas.Matcher<${EntityType}>)Entitas.Matcher<${EntityType}>.AllOf(${Index});\n                matcher.componentNames = ${componentNames};\n                _matcher${ComponentName} = matcher;\n            }\n\n            return _matcher${ComponentName};\n        }\n    }\n}\n";

	public override string name => "Component (Matcher API)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.ShouldGenerateIndex()
			select d).SelectMany(generate).ToArray();
	}

	private CodeGenFile[] generate(ComponentData data)
	{
		return (from context in data.GetContextNames()
			select generate(context, data)).ToArray();
	}

	private CodeGenFile generate(string contextName, ComponentData data)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		string text = "public sealed partial class ${MatcherType} {\n\n    static Entitas.IMatcher<${EntityType}> _matcher${ComponentName};\n\n    public static Entitas.IMatcher<${EntityType}> ${ComponentName} {\n        get {\n            if (_matcher${ComponentName} == null) {\n                var matcher = (Entitas.Matcher<${EntityType}>)Entitas.Matcher<${EntityType}>.AllOf(${Index});\n                matcher.componentNames = ${componentNames};\n                _matcher${ComponentName} = matcher;\n            }\n\n            return _matcher${ComponentName};\n        }\n    }\n}\n".Replace("${componentNames}", contextName + "ComponentsLookup.componentNames").Replace(data, contextName);
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + EntitasStringExtension.AddComponentSuffix(data.ComponentNameWithContext(contextName)) + ".cs", text, GetType().FullName);
	}
}
