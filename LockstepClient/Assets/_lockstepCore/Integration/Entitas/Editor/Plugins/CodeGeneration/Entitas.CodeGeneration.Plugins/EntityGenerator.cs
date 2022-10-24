using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class EntityGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string TEMPLATE = "public sealed partial class ${EntityType} : Entitas.Entity {\n}\n";

	public string name => "Entity";

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
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + EntitasStringExtension.AddEntitySuffix(contextName) + ".cs", "public sealed partial class ${EntityType} : Entitas.Entity {\n}\n".Replace(contextName), GetType().FullName);
	}
}
