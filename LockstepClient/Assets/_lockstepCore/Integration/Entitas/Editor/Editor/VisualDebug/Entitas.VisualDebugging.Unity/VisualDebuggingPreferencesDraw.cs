using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class VisualDebuggingPreferencesDrawer : AbstractPreferencesDrawer
{
	private const string ENTITAS_DISABLE_VISUAL_DEBUGGING = "ENTITAS_DISABLE_VISUAL_DEBUGGING";

	private const string ENTITAS_DISABLE_DEEP_PROFILING = "ENTITAS_DISABLE_DEEP_PROFILING";

	private VisualDebuggingConfig _visualDebuggingConfig;

	private ScriptingDefineSymbols _scriptingDefineSymbols;

	private bool _enableVisualDebugging;

	private bool _enableDeviceDeepProfiling;

	public override string title => "Visual Debugging";

	public override void Initialize(Preferences preferences)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		_visualDebuggingConfig = ConfigurableConfigExtension.CreateAndConfigure<VisualDebuggingConfig>(preferences);
		preferences.get_properties().AddProperties(((AbstractConfigurableConfig)_visualDebuggingConfig).get_defaultProperties(), false);
		_scriptingDefineSymbols = new ScriptingDefineSymbols();
		_enableVisualDebugging = !_scriptingDefineSymbols.get_buildTargetToDefSymbol().Values.All((string defs) => defs.Contains("ENTITAS_DISABLE_VISUAL_DEBUGGING"));
		_enableDeviceDeepProfiling = !_scriptingDefineSymbols.get_buildTargetToDefSymbol().Values.All((string defs) => defs.Contains("ENTITAS_DISABLE_DEEP_PROFILING"));
	}

	public override void DrawHeader(Preferences preferences)
	{
	}

	protected override void drawContent(Preferences preferences)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		drawVisualDebugging();
		if (GUILayout.Button("Show Stats", EditorStyles.get_miniButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			EntitasStats.ShowStats();
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		_visualDebuggingConfig.systemWarningThreshold = EditorGUILayout.IntField("System Warning Threshold", _visualDebuggingConfig.systemWarningThreshold, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.Space();
		drawDefaultInstanceCreator();
		drawTypeDrawerFolder();
	}

	private void drawVisualDebugging()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUI.BeginChangeCheck();
		_enableVisualDebugging = EditorGUILayout.Toggle("Enable Visual Debugging", _enableVisualDebugging, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorGUI.EndChangeCheck())
		{
			if (_enableVisualDebugging)
			{
				_scriptingDefineSymbols.RemoveDefineSymbol("ENTITAS_DISABLE_VISUAL_DEBUGGING");
			}
			else
			{
				_scriptingDefineSymbols.AddDefineSymbol("ENTITAS_DISABLE_VISUAL_DEBUGGING");
			}
		}
		EditorGUI.BeginChangeCheck();
		_enableDeviceDeepProfiling = EditorGUILayout.Toggle("Enable Device Profiling", _enableDeviceDeepProfiling, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorGUI.EndChangeCheck())
		{
			if (_enableDeviceDeepProfiling)
			{
				_scriptingDefineSymbols.RemoveDefineSymbol("ENTITAS_DISABLE_DEEP_PROFILING");
			}
			else
			{
				_scriptingDefineSymbols.AddDefineSymbol("ENTITAS_DISABLE_DEEP_PROFILING");
			}
		}
		EditorGUILayout.EndVertical();
	}

	private void drawDefaultInstanceCreator()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		string text = EditorLayout.ObjectFieldOpenFolderPanel("Default Instance Creators", _visualDebuggingConfig.defaultInstanceCreatorFolderPath, _visualDebuggingConfig.defaultInstanceCreatorFolderPath);
		if (!string.IsNullOrEmpty(text))
		{
			_visualDebuggingConfig.defaultInstanceCreatorFolderPath = text;
		}
		if (EditorLayout.MiniButton("New"))
		{
			EntityDrawer.GenerateIDefaultInstanceCreator("MyType");
		}
		EditorGUILayout.EndHorizontal();
	}

	private void drawTypeDrawerFolder()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		string text = EditorLayout.ObjectFieldOpenFolderPanel("Type Drawers", _visualDebuggingConfig.typeDrawerFolderPath, _visualDebuggingConfig.typeDrawerFolderPath);
		if (!string.IsNullOrEmpty(text))
		{
			_visualDebuggingConfig.typeDrawerFolderPath = text;
		}
		if (EditorLayout.MiniButton("New"))
		{
			EntityDrawer.GenerateITypeDrawer("MyType");
		}
		EditorGUILayout.EndHorizontal();
	}
}
