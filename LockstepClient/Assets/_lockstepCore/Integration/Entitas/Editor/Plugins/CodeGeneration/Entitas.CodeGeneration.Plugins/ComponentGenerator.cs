using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string COMPONENT_TEMPLATE = "[Entitas.CodeGeneration.Attributes.DontGenerate(false)]\npublic sealed class ${FullComponentName} : Entitas.IComponent {\n    public ${Type} value;\n}\n";

	public string name => "Component";

	public int priority => 0;

	public bool runInDryMode => true;

	public CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.ShouldGenerateComponent()
			select d).Select(generate).ToArray();
	}

	private CodeGenFile generate(ComponentData data)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		string text = SerializationTypeExtension.RemoveDots(data.GetTypeName());
		return new CodeGenFile("Components" + Path.DirectorySeparatorChar + text + ".cs", "[Entitas.CodeGeneration.Attributes.DontGenerate(false)]\npublic sealed class ${FullComponentName} : Entitas.IComponent {\n    public ${Type} value;\n}\n".Replace("${FullComponentName}", text).Replace("${Type}", data.GetObjectTypeName()), GetType().FullName);
	}
}
