using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.Editor;

public class EntitasPreferencesDrawer : AbstractPreferencesDrawer
{
	private enum AERCMode
	{
		Safe,
		FastAndUnsafe
	}

	private const string ENTITAS_FAST_AND_UNSAFE = "ENTITAS_FAST_AND_UNSAFE";

	private Texture2D _headerTexture;

	private ScriptingDefineSymbols _scriptingDefineSymbols;

	private AERCMode _aercMode;

	public override string title => "Entitas";

	public override void Initialize(Preferences preferences)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		_headerTexture = EditorLayout.LoadTexture("l:EntitasHeader");
		_scriptingDefineSymbols = new ScriptingDefineSymbols();
		_aercMode = (_scriptingDefineSymbols.get_buildTargetToDefSymbol().Values.All((string defs) => defs.Contains("ENTITAS_FAST_AND_UNSAFE")) ? AERCMode.FastAndUnsafe : AERCMode.Safe);
	}

	public override void DrawHeader(Preferences preferences)
	{
		drawToolbar();
		drawHeader(preferences);
	}

	private void drawToolbar()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal(EditorStyles.get_toolbar(), (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.ExpandWidth(true) });
		if (GUILayout.Button("Check for Updates", EditorStyles.get_toolbarButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			CheckForUpdates.DisplayUpdates();
		}
		if (GUILayout.Button("Chat", EditorStyles.get_toolbarButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			EntitasFeedback.EntitasChat();
		}
		if (GUILayout.Button("Docs", EditorStyles.get_toolbarButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			EntitasFeedback.EntitasDocs();
		}
		if (GUILayout.Button("Wiki", EditorStyles.get_toolbarButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			EntitasFeedback.EntitasWiki();
		}
		if (GUILayout.Button("Donate", EditorStyles.get_toolbarButton(), (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			EntitasFeedback.Donate();
		}
		EditorGUILayout.EndHorizontal();
	}

	private void drawHeader(Preferences preferences)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorLayout.DrawTexture(_headerTexture);
	}

	protected override void drawContent(Preferences preferences)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField("Automatic Entity Reference Counting", (GUILayoutOption[])(object)new GUILayoutOption[0]);
		GUIStyle val = new GUIStyle(EditorStyles.get_miniButtonLeft());
		if (_aercMode == AERCMode.Safe)
		{
			val.set_normal(val.get_active());
		}
		if (GUILayout.Button("Safe", val, (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			_aercMode = AERCMode.Safe;
			_scriptingDefineSymbols.RemoveDefineSymbol("ENTITAS_FAST_AND_UNSAFE");
		}
		val = new GUIStyle(EditorStyles.get_miniButtonRight());
		if (_aercMode == AERCMode.FastAndUnsafe)
		{
			val.set_normal(val.get_active());
		}
		if (GUILayout.Button("Fast And Unsafe", val, (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			_aercMode = AERCMode.FastAndUnsafe;
			_scriptingDefineSymbols.AddDefineSymbol("ENTITAS_FAST_AND_UNSAFE");
		}
		EditorGUILayout.EndHorizontal();
	}
}
