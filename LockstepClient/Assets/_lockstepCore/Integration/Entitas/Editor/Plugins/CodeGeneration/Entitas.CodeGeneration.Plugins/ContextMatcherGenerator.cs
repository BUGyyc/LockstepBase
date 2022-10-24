using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ContextMatcherGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string TEMPLATE = "public sealed partial class ${MatcherType} {\n\n    public static Entitas.IAllOfMatcher<${EntityType}> AllOf(params int[] indices) {\n        return Entitas.Matcher<${EntityType}>.AllOf(indices);\n    }\n\n    public static Entitas.IAllOfMatcher<${EntityType}> AllOf(params Entitas.IMatcher<${EntityType}>[] matchers) {\n          return Entitas.Matcher<${EntityType}>.AllOf(matchers);\n    }\n\n    public static Entitas.IAnyOfMatcher<${EntityType}> AnyOf(params int[] indices) {\n          return Entitas.Matcher<${EntityType}>.AnyOf(indices);\n    }\n\n    public static Entitas.IAnyOfMatcher<${EntityType}> AnyOf(params Entitas.IMatcher<${EntityType}>[] matchers) {\n          return Entitas.Matcher<${EntityType}>.AnyOf(matchers);\n    }\n}\n";

	public string name => "Context (Matcher API)";

	public int priority => 0;

	public bool runInDryMode => true;

	public CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return data.OfType<ContextData>().Select(generate).ToArray();
	}

	private CodeGenFile generate(ContextData data)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		string contextName = data.GetContextName();
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + EntitasStringExtension.AddMatcherSuffix(contextName) + ".cs", "public sealed partial class ${MatcherType} {\n\n    public static Entitas.IAllOfMatcher<${EntityType}> AllOf(params int[] indices) {\n        return Entitas.Matcher<${EntityType}>.AllOf(indices);\n    }\n\n    public static Entitas.IAllOfMatcher<${EntityType}> AllOf(params Entitas.IMatcher<${EntityType}>[] matchers) {\n          return Entitas.Matcher<${EntityType}>.AllOf(matchers);\n    }\n\n    public static Entitas.IAnyOfMatcher<${EntityType}> AnyOf(params int[] indices) {\n          return Entitas.Matcher<${EntityType}>.AnyOf(indices);\n    }\n\n    public static Entitas.IAnyOfMatcher<${EntityType}> AnyOf(params Entitas.IMatcher<${EntityType}>[] matchers) {\n          return Entitas.Matcher<${EntityType}>.AnyOf(matchers);\n    }\n}\n".Replace(contextName), GetType().FullName);
	}
}
