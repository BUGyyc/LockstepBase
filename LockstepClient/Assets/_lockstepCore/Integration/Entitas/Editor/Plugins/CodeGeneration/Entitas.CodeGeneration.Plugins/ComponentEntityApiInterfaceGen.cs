using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentEntityApiInterfaceGenerator : AbstractGenerator
{
	private const string STANDARD_TEMPLATE = "public partial interface I${ComponentName}Entity {\n\n    ${ComponentType} ${validComponentName} { get; }\n    bool has${ComponentName} { get; }\n\n    void Add${ComponentName}(${newMethodParameters});\n    void Replace${ComponentName}(${newMethodParameters});\n    void Remove${ComponentName}();\n}\n";

	private const string FLAG_TEMPLATE = "public partial interface I${ComponentName}Entity {\n    bool ${prefixedComponentName} { get; set; }\n}\n";

	private const string ENTITY_INTERFACE_TEMPLATE = "public partial class ${EntityType} : I${ComponentName}Entity { }\n";

	public override string name => "Component (Entity API Interface)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.ShouldGenerateMethods()
			where d.GetContextNames().Length > 1
			select d).SelectMany(generate).ToArray();
	}

	private CodeGenFile[] generate(ComponentData data)
	{
		return ((IEnumerable<CodeGenFile>)(object)new CodeGenFile[1] { generateInterface(data) }).Concat(from contextName in data.GetContextNames()
			select generateEntityInterface(contextName, data)).ToArray();
	}

	private CodeGenFile generateInterface(ComponentData data)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		string template = ((data.GetMemberData().Length == 0) ? "public partial interface I${ComponentName}Entity {\n    bool ${prefixedComponentName} { get; set; }\n}\n" : "public partial interface I${ComponentName}Entity {\n\n    ${ComponentType} ${validComponentName} { get; }\n    bool has${ComponentName} { get; }\n\n    void Add${ComponentName}(${newMethodParameters});\n    void Replace${ComponentName}(${newMethodParameters});\n    void Remove${ComponentName}();\n}\n");
		return new CodeGenFile("Components" + Path.DirectorySeparatorChar + "Interfaces" + Path.DirectorySeparatorChar + "I" + data.ComponentName() + "Entity.cs", template.Replace(data, string.Empty), GetType().FullName);
	}

	private CodeGenFile generateEntityInterface(string contextName, ComponentData data)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + EntitasStringExtension.AddComponentSuffix(data.ComponentNameWithContext(contextName)) + ".cs", "public partial class ${EntityType} : I${ComponentName}Entity { }\n".Replace(data, contextName), GetType().FullName);
	}
}
