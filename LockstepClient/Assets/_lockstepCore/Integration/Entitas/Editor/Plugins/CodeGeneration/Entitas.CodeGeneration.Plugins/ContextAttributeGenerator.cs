using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ContextAttributeGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string TEMPLATE = "public sealed class ${ContextName}Attribute : Entitas.CodeGeneration.Attributes.ContextAttribute {\n\n    public ${ContextName}Attribute() : base(\"${ContextName}\") {\n    }\n}\n";

	public string name => "Context (Attribute)";

	public int priority => 0;

	public bool runInDryMode => true;

	public CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return data.OfType<ContextData>().Select(generate).ToArray();
	}

	private CodeGenFile generate(ContextData data)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		string contextName = data.GetContextName();
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + contextName + "Attribute.cs", "public sealed class ${ContextName}Attribute : Entitas.CodeGeneration.Attributes.ContextAttribute {\n\n    public ${ContextName}Attribute() : base(\"${ContextName}\") {\n    }\n}\n".Replace(contextName), GetType().FullName);
	}
}
