using System.IO;

namespace Entitas.Migration;

public class M0410_2 : IMigration
{
	public string version => "0.41.0-2";

	public string workingDirectory => "where generated files are located";

	public string description => "Adding temporary Feature class";

	public MigrationFile[] Migrate(string path)
	{
		return new MigrationFile[1]
		{
			new MigrationFile(path + Path.DirectorySeparatorChar + "Feature.cs", "namespace Entitas {\n\n#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)\n\n    public class Feature : Entitas.VisualDebugging.Unity.DebugSystems {\n\n        public Feature(string name) : base(name) {\n        }\n\n        public Feature() : base(true) {\n            var typeName = Entitas.Utils.TypeSerializationExtension.ToCompilableString(GetType());\n            var shortType = Entitas.Utils.TypeSerializationExtension.ShortTypeName(typeName);\n            initialize(toSpacedCamelCase(shortType));\n        }\n\n        static string toSpacedCamelCase(string text) {\n            var sb = new System.Text.StringBuilder(text.Length * 2);\n            sb.Append(char.ToUpper(text[0]));\n            for (int i = 1; i < text.Length; i++) {\n                if (char.IsUpper(text[i]) && text[i - 1] != ' ') {\n                    sb.Append(' ');\n                }\n                sb.Append(text[i]);\n            }\n\n            return sb.ToString();\n        }\n    }\n\n#else\n\n    public class Feature : Entitas.Systems {\n\n        public Feature(string name) {\n        }\n\n        public Feature() {\n        }\n    }\n\n#endif\n\n}")
		};
	}
}
