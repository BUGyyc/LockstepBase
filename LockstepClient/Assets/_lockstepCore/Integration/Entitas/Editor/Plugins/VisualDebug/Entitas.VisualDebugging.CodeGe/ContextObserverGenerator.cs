using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Utils;
using Entitas.CodeGeneration.Plugins;

namespace Entitas.VisualDebugging.CodeGeneration.Plugins;

public class ContextObserverGenerator : ICodeGenerator, ICodeGenerationPlugin
{
	private const string CONTEXTS_TEMPLATE = "public partial class Contexts {\n\n#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)\n\n    [Entitas.CodeGeneration.Attributes.PostConstructor]\n    public void InitializeContextObservers() {\n        try {\n${contextObservers}\n        } catch(System.Exception) {\n        }\n    }\n\n    public void CreateContextObserver(Entitas.IContext context) {\n        if (UnityEngine.Application.isPlaying) {\n            var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);\n            UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);\n        }\n    }\n\n#endif\n}\n";

	private const string CONTEXT_OBSERVER_TEMPLATE = "            CreateContextObserver(${contextName});";

	public string name => "Context Observer";

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
			new CodeGenFile("Contexts.cs", generateContextsClass(contextNames), GetType().FullName)
		};
	}

	private string generateContextsClass(string[] contextNames)
	{
		string newValue = string.Join("\n", contextNames.Select((string contextName) => "            CreateContextObserver(${contextName});".Replace("${contextName}", StringExtension.LowercaseFirst(contextName))).ToArray());
		return "public partial class Contexts {\n\n#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)\n\n    [Entitas.CodeGeneration.Attributes.PostConstructor]\n    public void InitializeContextObservers() {\n        try {\n${contextObservers}\n        } catch(System.Exception) {\n        }\n    }\n\n    public void CreateContextObserver(Entitas.IContext context) {\n        if (UnityEngine.Application.isPlaying) {\n            var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);\n            UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);\n        }\n    }\n\n#endif\n}\n".Replace("${contextObservers}", newValue);
	}
}
