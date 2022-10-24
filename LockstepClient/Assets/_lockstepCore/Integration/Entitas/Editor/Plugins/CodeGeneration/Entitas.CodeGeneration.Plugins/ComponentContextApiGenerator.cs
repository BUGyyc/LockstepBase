using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentContextApiGenerator : AbstractGenerator
{
	private const string STANDARD_TEMPLATE = "public partial class ${ContextType} {\n\n    public ${EntityType} ${componentName}Entity { get { return GetGroup(${MatcherType}.${ComponentName}).GetSingleEntity(); } }\n    public ${ComponentType} ${validComponentName} { get { return ${componentName}Entity.${componentName}; } }\n    public bool has${ComponentName} { get { return ${componentName}Entity != null; } }\n\n    public ${EntityType} Set${ComponentName}(${newMethodParameters}) {\n        if (has${ComponentName}) {\n            throw new Entitas.EntitasException(\"Could not set ${ComponentName}!\\n\" + this + \" already has an entity with ${ComponentType}!\",\n                \"You should check if the context already has a ${componentName}Entity before setting it or use context.Replace${ComponentName}().\");\n        }\n        var entity = CreateEntity();\n        entity.Add${ComponentName}(${newMethodArgs});\n        return entity;\n    }\n\n    public void Replace${ComponentName}(${newMethodParameters}) {\n        var entity = ${componentName}Entity;\n        if (entity == null) {\n            entity = Set${ComponentName}(${newMethodArgs});\n        } else {\n            entity.Replace${ComponentName}(${newMethodArgs});\n        }\n    }\n\n    public void Remove${ComponentName}() {\n        ${componentName}Entity.Destroy();\n    }\n}\n";

	private const string FLAG_TEMPLATE = "public partial class ${ContextType} {\n\n    public ${EntityType} ${componentName}Entity { get { return GetGroup(${MatcherType}.${ComponentName}).GetSingleEntity(); } }\n\n    public bool ${prefixedComponentName} {\n        get { return ${componentName}Entity != null; }\n        set {\n            var entity = ${componentName}Entity;\n            if (value != (entity != null)) {\n                if (value) {\n                    CreateEntity().${prefixedComponentName} = true;\n                } else {\n                    entity.Destroy();\n                }\n            }\n        }\n    }\n}\n";

	public override string name => "Component (Context API)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.ShouldGenerateMethods()
			where d.IsUnique()
			select d).SelectMany(generate).ToArray();
	}

	private CodeGenFile[] generate(ComponentData data)
	{
		return (from contextName in data.GetContextNames()
			select generate(contextName, data)).ToArray();
	}

	private CodeGenFile generate(string contextName, ComponentData data)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		string template = ((data.GetMemberData().Length == 0) ? "public partial class ${ContextType} {\n\n    public ${EntityType} ${componentName}Entity { get { return GetGroup(${MatcherType}.${ComponentName}).GetSingleEntity(); } }\n\n    public bool ${prefixedComponentName} {\n        get { return ${componentName}Entity != null; }\n        set {\n            var entity = ${componentName}Entity;\n            if (value != (entity != null)) {\n                if (value) {\n                    CreateEntity().${prefixedComponentName} = true;\n                } else {\n                    entity.Destroy();\n                }\n            }\n        }\n    }\n}\n" : "public partial class ${ContextType} {\n\n    public ${EntityType} ${componentName}Entity { get { return GetGroup(${MatcherType}.${ComponentName}).GetSingleEntity(); } }\n    public ${ComponentType} ${validComponentName} { get { return ${componentName}Entity.${componentName}; } }\n    public bool has${ComponentName} { get { return ${componentName}Entity != null; } }\n\n    public ${EntityType} Set${ComponentName}(${newMethodParameters}) {\n        if (has${ComponentName}) {\n            throw new Entitas.EntitasException(\"Could not set ${ComponentName}!\\n\" + this + \" already has an entity with ${ComponentType}!\",\n                \"You should check if the context already has a ${componentName}Entity before setting it or use context.Replace${ComponentName}().\");\n        }\n        var entity = CreateEntity();\n        entity.Add${ComponentName}(${newMethodArgs});\n        return entity;\n    }\n\n    public void Replace${ComponentName}(${newMethodParameters}) {\n        var entity = ${componentName}Entity;\n        if (entity == null) {\n            entity = Set${ComponentName}(${newMethodArgs});\n        } else {\n            entity.Replace${ComponentName}(${newMethodArgs});\n        }\n    }\n\n    public void Remove${ComponentName}() {\n        ${componentName}Entity.Destroy();\n    }\n}\n");
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + EntitasStringExtension.AddComponentSuffix(data.ComponentNameWithContext(contextName)) + ".cs", template.Replace(data, contextName), GetType().FullName);
	}
}
