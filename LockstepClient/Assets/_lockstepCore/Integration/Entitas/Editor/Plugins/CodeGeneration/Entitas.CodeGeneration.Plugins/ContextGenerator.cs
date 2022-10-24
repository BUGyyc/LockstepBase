using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ContextGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string TEMPLATE = "public sealed partial class ${ContextType} : Entitas.Context<${EntityType}> {\n\n    public ${ContextType}()\n        : base(\n            ${Lookup}.TotalComponents,\n            0,\n            new Entitas.ContextInfo(\n                \"${ContextName}\",\n                ${Lookup}.componentNames,\n                ${Lookup}.componentTypes\n            ),\n            (entity) =>\n\n#if (ENTITAS_FAST_AND_UNSAFE)\n                new Entitas.UnsafeAERC(),\n#else\n                new Entitas.SafeAERC(entity),\n#endif\n            () => new ${EntityType}()\n        ) {\n    }\n}\n";

	public string name => "Context";

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
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + EntitasStringExtension.AddContextSuffix(contextName) + ".cs", "public sealed partial class ${ContextType} : Entitas.Context<${EntityType}> {\n\n    public ${ContextType}()\n        : base(\n            ${Lookup}.TotalComponents,\n            0,\n            new Entitas.ContextInfo(\n                \"${ContextName}\",\n                ${Lookup}.componentNames,\n                ${Lookup}.componentTypes\n            ),\n            (entity) =>\n\n#if (ENTITAS_FAST_AND_UNSAFE)\n                new Entitas.UnsafeAERC(),\n#else\n                new Entitas.SafeAERC(entity),\n#endif\n            () => new ${EntityType}()\n        ) {\n    }\n}\n".Replace(contextName), GetType().FullName);
	}
}
