using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentEntityApiGenerator : AbstractGenerator
{
	private const string STANDARD_TEMPLATE = "public partial class ${EntityType} {\n\n    public ${ComponentType} ${validComponentName} { get { return (${ComponentType})GetComponent(${Index}); } }\n    public bool has${ComponentName} { get { return HasComponent(${Index}); } }\n\n    public void Add${ComponentName}(${newMethodParameters}) {\n        var index = ${Index};\n        var component = (${ComponentType})CreateComponent(index, typeof(${ComponentType}));\n${memberAssignmentList}\n        AddComponent(index, component);\n    }\n\n    public void Replace${ComponentName}(${newMethodParameters}) {\n        var index = ${Index};\n        var component = (${ComponentType})CreateComponent(index, typeof(${ComponentType}));\n${memberAssignmentList}\n        ReplaceComponent(index, component);\n    }\n\n    public void Remove${ComponentName}() {\n        RemoveComponent(${Index});\n    }\n}\n";

	private const string FLAG_TEMPLATE = "public partial class ${EntityType} {\n\n    static readonly ${ComponentType} ${componentName}Component = new ${ComponentType}();\n\n    public bool ${prefixedComponentName} {\n        get { return HasComponent(${Index}); }\n        set {\n            if (value != ${prefixedComponentName}) {\n                var index = ${Index};\n                if (value) {\n                    var componentPool = GetComponentPool(index);\n                    var component = componentPool.Count > 0\n                            ? componentPool.Pop()\n                            : ${componentName}Component;\n\n                    AddComponent(index, component);\n                } else {\n                    RemoveComponent(index);\n                }\n            }\n        }\n    }\n}\n";

	public override string name => "Component (Entity API)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.ShouldGenerateMethods()
			select d).SelectMany(generate).ToArray();
	}

	private CodeGenFile[] generate(ComponentData data)
	{
		return (from contextName in data.GetContextNames()
			select generate(contextName, data)).ToArray();
	}

	private CodeGenFile generate(string contextName, ComponentData data)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		string text = ((data.GetMemberData().Length == 0) ? "public partial class ${EntityType} {\n\n    static readonly ${ComponentType} ${componentName}Component = new ${ComponentType}();\n\n    public bool ${prefixedComponentName} {\n        get { return HasComponent(${Index}); }\n        set {\n            if (value != ${prefixedComponentName}) {\n                var index = ${Index};\n                if (value) {\n                    var componentPool = GetComponentPool(index);\n                    var component = componentPool.Count > 0\n                            ? componentPool.Pop()\n                            : ${componentName}Component;\n\n                    AddComponent(index, component);\n                } else {\n                    RemoveComponent(index);\n                }\n            }\n        }\n    }\n}\n" : "public partial class ${EntityType} {\n\n    public ${ComponentType} ${validComponentName} { get { return (${ComponentType})GetComponent(${Index}); } }\n    public bool has${ComponentName} { get { return HasComponent(${Index}); } }\n\n    public void Add${ComponentName}(${newMethodParameters}) {\n        var index = ${Index};\n        var component = (${ComponentType})CreateComponent(index, typeof(${ComponentType}));\n${memberAssignmentList}\n        AddComponent(index, component);\n    }\n\n    public void Replace${ComponentName}(${newMethodParameters}) {\n        var index = ${Index};\n        var component = (${ComponentType})CreateComponent(index, typeof(${ComponentType}));\n${memberAssignmentList}\n        ReplaceComponent(index, component);\n    }\n\n    public void Remove${ComponentName}() {\n        RemoveComponent(${Index});\n    }\n}\n").Replace("${memberAssignmentList}", getMemberAssignmentList(data.GetMemberData())).Replace(data, contextName);
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + EntitasStringExtension.AddComponentSuffix(data.ComponentNameWithContext(contextName)) + ".cs", text, GetType().FullName);
	}

	private string getMemberAssignmentList(MemberData[] memberData)
	{
		return string.Join("\n", memberData.Select((MemberData info) => "        component." + info.name + " = new" + StringExtension.UppercaseFirst(info.name) + ";").ToArray());
	}
}
