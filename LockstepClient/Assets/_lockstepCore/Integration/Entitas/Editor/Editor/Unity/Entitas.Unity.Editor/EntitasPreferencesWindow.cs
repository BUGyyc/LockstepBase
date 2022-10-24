using System;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.Editor;

public class EntitasPreferencesWindow : PreferencesWindow
{
	[MenuItem("Tools/Entitas/Preferences... #%e", false, 1)]
	public static void OpenPreferences()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		EntitasPreferencesWindow window = EditorWindow.GetWindow<EntitasPreferencesWindow>(true, "Entitas " + CheckForUpdates.GetLocalVersion());
		((EditorWindow)window).set_minSize(new Vector2(415f, 348f));
		((PreferencesWindow)window).Initialize("Entitas.properties", Environment.UserName + ".userproperties", new string[2] { "Entitas.Unity.Editor.EntitasPreferencesDrawer", "Entitas.VisualDebugging.Unity.Editor.VisualDebuggingPreferencesDrawer" });
		((EditorWindow)window).Show();
	}
}
