using System.Collections.Generic;
using DesperateDevs.Serialization;

namespace Entitas.VisualDebugging.Unity.Editor;

public class VisualDebuggingConfig : AbstractConfigurableConfig
{
	private const string SYSTEM_WARNING_THRESHOLD_KEY = "Entitas.VisualDebugging.Unity.Editor.SystemWarningThreshold";

	private const string DEFAULT_INSTANCE_CREATOR_FOLDER_PATH_KEY = "Entitas.VisualDebugging.Unity.Editor.DefaultInstanceCreatorFolderPath";

	private const string TYPE_DRAWER_FOLDER_PATH_KEY = "Entitas.VisualDebugging.Unity.Editor.TypeDrawerFolderPath";

	public override Dictionary<string, string> defaultProperties => new Dictionary<string, string>
	{
		{ "Entitas.VisualDebugging.Unity.Editor.SystemWarningThreshold", "5" },
		{ "Entitas.VisualDebugging.Unity.Editor.DefaultInstanceCreatorFolderPath", "Assets/Editor/DefaultInstanceCreator" },
		{ "Entitas.VisualDebugging.Unity.Editor.TypeDrawerFolderPath", "Assets/Editor/TypeDrawer" }
	};

	public int systemWarningThreshold
	{
		get
		{
			return int.Parse(base._preferences.get_Item("Entitas.VisualDebugging.Unity.Editor.SystemWarningThreshold"));
		}
		set
		{
			base._preferences.set_Item("Entitas.VisualDebugging.Unity.Editor.SystemWarningThreshold", value.ToString());
		}
	}

	public string defaultInstanceCreatorFolderPath
	{
		get
		{
			return base._preferences.get_Item("Entitas.VisualDebugging.Unity.Editor.DefaultInstanceCreatorFolderPath");
		}
		set
		{
			base._preferences.set_Item("Entitas.VisualDebugging.Unity.Editor.DefaultInstanceCreatorFolderPath", value);
		}
	}

	public string typeDrawerFolderPath
	{
		get
		{
			return base._preferences.get_Item("Entitas.VisualDebugging.Unity.Editor.TypeDrawerFolderPath");
		}
		set
		{
			base._preferences.set_Item("Entitas.VisualDebugging.Unity.Editor.TypeDrawerFolderPath", value);
		}
	}
}
