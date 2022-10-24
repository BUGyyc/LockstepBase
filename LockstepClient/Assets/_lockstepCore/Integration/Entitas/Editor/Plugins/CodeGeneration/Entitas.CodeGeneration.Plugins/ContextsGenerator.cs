using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ContextsGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string TEMPLATE = "public partial class Contexts : Entitas.IContexts {\n\n    public static Contexts sharedInstance {\n        get {\n            if (_sharedInstance == null) {\n                _sharedInstance = new Contexts();\n            }\n\n            return _sharedInstance;\n        }\n        set { _sharedInstance = value; }\n    }\n\n    static Contexts _sharedInstance;\n\n${contextPropertiesList}\n\n    public Entitas.IContext[] allContexts { get { return new Entitas.IContext [] { ${contextList} }; } }\n\n    public Contexts() {\n${contextAssignmentsList}\n\n        var postConstructors = System.Linq.Enumerable.Where(\n            GetType().GetMethods(),\n            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostConstructorAttribute))\n        );\n\n        foreach (var postConstructor in postConstructors) {\n            postConstructor.Invoke(this, null);\n        }\n    }\n\n    public void Reset() {\n        var contexts = allContexts;\n        for (int i = 0; i < contexts.Length; i++) {\n            contexts[i].Reset();\n        }\n    }\n}\n";

	private const string CONTEXT_PROPERTY_TEMPLATE = "    public ${ContextType} ${contextName} { get; set; }";

	private const string CONTEXT_LIST_TEMPLATE = "${contextName}";

	private const string CONTEXT_ASSIGNMENT_TEMPLATE = "        ${contextName} = new ${ContextType}();";

	public string name => "Contexts";

	public int priority => 0;

	public bool runInDryMode => true;

	public CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		string[] contextNames = (from d in data.OfType<ContextData>()
			select d.GetContextName() into contextName
			orderby contextName
			select contextName).ToArray();
		return (CodeGenFile[])(object)new CodeGenFile[1]
		{
			new CodeGenFile("Contexts.cs", generate(contextNames), GetType().FullName)
		};
	}

	private string generate(string[] contextNames)
	{
		string newValue = string.Join("\n", contextNames.Select((string contextName) => "    public ${ContextType} ${contextName} { get; set; }".Replace(contextName)).ToArray());
		string newValue2 = string.Join(", ", contextNames.Select((string contextName) => "${contextName}".Replace(contextName)).ToArray());
		string newValue3 = string.Join("\n", contextNames.Select((string contextName) => "        ${contextName} = new ${ContextType}();".Replace(contextName)).ToArray());
		return "public partial class Contexts : Entitas.IContexts {\n\n    public static Contexts sharedInstance {\n        get {\n            if (_sharedInstance == null) {\n                _sharedInstance = new Contexts();\n            }\n\n            return _sharedInstance;\n        }\n        set { _sharedInstance = value; }\n    }\n\n    static Contexts _sharedInstance;\n\n${contextPropertiesList}\n\n    public Entitas.IContext[] allContexts { get { return new Entitas.IContext [] { ${contextList} }; } }\n\n    public Contexts() {\n${contextAssignmentsList}\n\n        var postConstructors = System.Linq.Enumerable.Where(\n            GetType().GetMethods(),\n            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostConstructorAttribute))\n        );\n\n        foreach (var postConstructor in postConstructors) {\n            postConstructor.Invoke(this, null);\n        }\n    }\n\n    public void Reset() {\n        var contexts = allContexts;\n        for (int i = 0; i < contexts.Length; i++) {\n            contexts[i].Reset();\n        }\n    }\n}\n".Replace("${contextPropertiesList}", newValue).Replace("${contextList}", newValue2).Replace("${contextAssignmentsList}", newValue3);
	}
}
