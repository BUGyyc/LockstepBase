using System.IO;
using System.Linq;

namespace Entitas.CodeGeneration.Plugins;

public static class TemplateConfigExtension
{
	public static string FindTemplate(this TemplatesConfig config, string fileName)
	{
		string[] templates = config.templates;
		for (int i = 0; i < templates.Length; i++)
		{
			string text = Directory.GetFiles(templates[i], fileName, SearchOption.TopDirectoryOnly).FirstOrDefault();
			if (text != null)
			{
				return text;
			}
		}
		return null;
	}

	public static string FindComponentContextTemplate(this TemplatesConfig config)
	{
		return config.FindTemplate("ComponentContext.txt");
	}

	public static string FindFlagComponentContextTemplate(this TemplatesConfig config)
	{
		return config.FindTemplate("FlagComponentContext.txt");
	}

	public static string FindMethodArgumentTemplate(this TemplatesConfig config)
	{
		return config.FindTemplate("MethodArgument.txt");
	}

	public static string FindMethodParameterTemplate(this TemplatesConfig config)
	{
		return config.FindTemplate("MethodParameter.txt");
	}
}
