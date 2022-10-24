using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

[CustomEditor(typeof(DebugSystemsBehaviour))]
public class DebugSystemsInspector : Editor
{
	private enum SortMethod
	{
		OrderOfOccurrence,
		Name,
		NameDescending,
		ExecutionTime,
		ExecutionTimeDescending
	}

	private Graph _systemsMonitor;

	private Queue<float> _systemMonitorData;

	private const int SYSTEM_MONITOR_DATA_LENGTH = 60;

	private static bool _showDetails = false;

	private static bool _showSystemsMonitor = true;

	private static bool _showSystemsList = true;

	private static bool _showInitializeSystems = true;

	private static bool _showExecuteSystems = true;

	private static bool _showCleanupSystems = true;

	private static bool _showTearDownSystems = true;

	private static bool _hideEmptySystems = true;

	private static string _systemNameSearchString = string.Empty;

	private int _systemWarningThreshold;

	private float _threshold;

	private SortMethod _systemSortMethod;

	private int _lastRenderedFrameCount;

	private GUIContent _stepButtonContent;

	private GUIContent _pauseButtonContent;

	private void OnEnable()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		VisualDebuggingConfig visualDebuggingConfig = ConfigurableConfigExtension.CreateAndConfigure<VisualDebuggingConfig>(new Preferences("Entitas.properties", Environment.UserName + ".userproperties"));
		_systemWarningThreshold = visualDebuggingConfig.systemWarningThreshold;
	}

	public override void OnInspectorGUI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		DebugSystems systems = ((DebugSystemsBehaviour)((Editor)this).get_target()).get_systems();
		EditorGUILayout.Space();
		drawSystemsOverview(systems);
		EditorGUILayout.Space();
		drawSystemsMonitor(systems);
		EditorGUILayout.Space();
		drawSystemList(systems);
		EditorGUILayout.Space();
		EditorUtility.SetDirty(((Editor)this).get_target());
	}

	private static void drawSystemsOverview(DebugSystems systems)
	{
		_showDetails = EditorLayout.DrawSectionHeaderToggle("Details", _showDetails);
		if (_showDetails)
		{
			EditorLayout.BeginSectionContent();
			EditorGUILayout.LabelField(systems.get_name(), EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Initialize Systems", systems.get_totalInitializeSystemsCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Execute Systems", systems.get_totalExecuteSystemsCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Cleanup Systems", systems.get_totalCleanupSystemsCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("TearDown Systems", systems.get_totalTearDownSystemsCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Total Systems", systems.get_totalSystemsCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorLayout.EndSectionContent();
		}
	}

	private void drawSystemsMonitor(DebugSystems systems)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (_systemsMonitor == null)
		{
			_systemsMonitor = new Graph(60);
			_systemMonitorData = new Queue<float>(new float[60]);
		}
		_showSystemsMonitor = EditorLayout.DrawSectionHeaderToggle("Performance", _showSystemsMonitor);
		if (_showSystemsMonitor)
		{
			EditorLayout.BeginSectionContent();
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Execution duration", systems.get_executeDuration().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField("Cleanup duration", systems.get_cleanupDuration().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.EndVertical();
			if (_stepButtonContent == null)
			{
				_stepButtonContent = EditorGUIUtility.IconContent("StepButton On");
			}
			if (_pauseButtonContent == null)
			{
				_pauseButtonContent = EditorGUIUtility.IconContent("PauseButton On");
			}
			systems.paused = GUILayout.Toggle(systems.paused, _pauseButtonContent, GUIStyle.op_Implicit("CommandLeft"), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			if (GUILayout.Button(_stepButtonContent, GUIStyle.op_Implicit("CommandRight"), (GUILayoutOption[])(object)new GUILayoutOption[0]))
			{
				systems.paused = true;
				systems.StepExecute();
				systems.StepCleanup();
				addDuration((float)systems.get_executeDuration() + (float)systems.get_cleanupDuration());
			}
			EditorGUILayout.EndHorizontal();
			if (!EditorApplication.get_isPaused() && !systems.paused)
			{
				addDuration((float)systems.get_executeDuration() + (float)systems.get_cleanupDuration());
			}
			_systemsMonitor.Draw(_systemMonitorData.ToArray(), 80f);
			EditorLayout.EndSectionContent();
		}
	}

	private void drawSystemList(DebugSystems systems)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		_showSystemsList = EditorLayout.DrawSectionHeaderToggle("Systems", _showSystemsList);
		if (!_showSystemsList)
		{
			return;
		}
		EditorLayout.BeginSectionContent();
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		DebugSystems.avgResetInterval = (AvgResetInterval)(object)EditorGUILayout.EnumPopup("Reset average duration Ø", (Enum)(object)DebugSystems.avgResetInterval, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (GUILayout.Button("Reset Ø now", EditorStyles.get_miniButton(), (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(88f) }))
		{
			systems.ResetDurations();
		}
		EditorGUILayout.EndHorizontal();
		_threshold = EditorGUILayout.Slider("Threshold Ø ms", _threshold, 0f, 33f, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		_hideEmptySystems = EditorGUILayout.Toggle("Hide empty systems", _hideEmptySystems, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		_systemSortMethod = (SortMethod)(object)EditorGUILayout.EnumPopup((Enum)_systemSortMethod, EditorStyles.get_popup(), (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(150f) });
		_systemNameSearchString = EditorLayout.SearchTextField(_systemNameSearchString);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		_showInitializeSystems = EditorLayout.DrawSectionHeaderToggle("Initialize Systems", _showInitializeSystems);
		if (_showInitializeSystems && shouldShowSystems(systems, (SystemInterfaceFlags)2))
		{
			EditorLayout.BeginSectionContent();
			if (drawSystemInfos(systems, (SystemInterfaceFlags)2) == 0)
			{
				EditorGUILayout.LabelField(string.Empty, (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			EditorLayout.EndSectionContent();
		}
		_showExecuteSystems = EditorLayout.DrawSectionHeaderToggle("Execute Systems", _showExecuteSystems);
		if (_showExecuteSystems && shouldShowSystems(systems, (SystemInterfaceFlags)4))
		{
			EditorLayout.BeginSectionContent();
			if (drawSystemInfos(systems, (SystemInterfaceFlags)4) == 0)
			{
				EditorGUILayout.LabelField(string.Empty, (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			EditorLayout.EndSectionContent();
		}
		_showCleanupSystems = EditorLayout.DrawSectionHeaderToggle("Cleanup Systems", _showCleanupSystems);
		if (_showCleanupSystems && shouldShowSystems(systems, (SystemInterfaceFlags)8))
		{
			EditorLayout.BeginSectionContent();
			if (drawSystemInfos(systems, (SystemInterfaceFlags)8) == 0)
			{
				EditorGUILayout.LabelField(string.Empty, (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			EditorLayout.EndSectionContent();
		}
		_showTearDownSystems = EditorLayout.DrawSectionHeaderToggle("TearDown Systems", _showTearDownSystems);
		if (_showTearDownSystems && shouldShowSystems(systems, (SystemInterfaceFlags)16))
		{
			EditorLayout.BeginSectionContent();
			if (drawSystemInfos(systems, (SystemInterfaceFlags)16) == 0)
			{
				EditorGUILayout.LabelField(string.Empty, (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			EditorLayout.EndSectionContent();
		}
		EditorLayout.EndSectionContent();
	}

	private int drawSystemInfos(DebugSystems systems, SystemInterfaceFlags type)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Invalid comparison between Unknown and I4
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Invalid comparison between Unknown and I4
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Invalid comparison between Unknown and I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Invalid comparison between Unknown and I4
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Invalid comparison between Unknown and I4
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Invalid comparison between Unknown and I4
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		SystemInfo[] systemInfos = null;
		if ((int)type <= 4)
		{
			if ((int)type != 2)
			{
				if ((int)type == 4)
				{
					systemInfos = (from systemInfo in systems.get_executeSystemInfos()
						where systemInfo.get_averageExecutionDuration() >= (double)_threshold
						select systemInfo).ToArray();
				}
			}
			else
			{
				systemInfos = (from systemInfo in systems.get_initializeSystemInfos()
					where systemInfo.get_initializationDuration() >= (double)_threshold
					select systemInfo).ToArray();
			}
		}
		else if ((int)type != 8)
		{
			if ((int)type == 16)
			{
				systemInfos = (from systemInfo in systems.get_tearDownSystemInfos()
					where systemInfo.get_teardownDuration() >= (double)_threshold
					select systemInfo).ToArray();
			}
		}
		else
		{
			systemInfos = (from systemInfo in systems.get_cleanupSystemInfos()
				where systemInfo.get_cleanupDuration() >= (double)_threshold
				select systemInfo).ToArray();
		}
		systemInfos = getSortedSystemInfos(systemInfos, _systemSortMethod);
		int num = 0;
		SystemInfo[] array = systemInfos;
		foreach (SystemInfo val in array)
		{
			ISystem system = val.get_system();
			DebugSystems val2 = (DebugSystems)(object)((system is DebugSystems) ? system : null);
			if (val2 != null && !shouldShowSystems(val2, type))
			{
				continue;
			}
			if (EditorLayout.MatchesSearchString(val.get_systemName().ToLower(), _systemNameSearchString.ToLower()))
			{
				EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
				int indentLevel = EditorGUI.get_indentLevel();
				EditorGUI.set_indentLevel(0);
				bool isActive = val.isActive;
				if (val.get_areAllParentsActive())
				{
					val.isActive = EditorGUILayout.Toggle(val.isActive, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(20f) });
				}
				else
				{
					EditorGUI.BeginDisabledGroup(true);
					EditorGUILayout.Toggle(false, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(20f) });
				}
				EditorGUI.EndDisabledGroup();
				EditorGUI.set_indentLevel(indentLevel);
				if (val.isActive != isActive)
				{
					ISystem system2 = val.get_system();
					IReactiveSystem val3 = (IReactiveSystem)(object)((system2 is IReactiveSystem) ? system2 : null);
					if (val3 != null)
					{
						if (val.isActive)
						{
							val3.Activate();
						}
						else
						{
							val3.Deactivate();
						}
					}
				}
				if ((int)type <= 4)
				{
					if ((int)type != 2)
					{
						if ((int)type == 4)
						{
							string text = $"Ø {val.get_averageExecutionDuration():00.000}".PadRight(12);
							string text2 = $"▼ {val.get_minExecutionDuration():00.000}".PadRight(12);
							string text3 = $"▲ {val.get_maxExecutionDuration():00.000}";
							EditorGUILayout.LabelField(val.get_systemName(), text + text2 + text3, getSystemStyle(val, (SystemInterfaceFlags)4), (GUILayoutOption[])(object)new GUILayoutOption[0]);
						}
					}
					else
					{
						EditorGUILayout.LabelField(val.get_systemName(), val.get_initializationDuration().ToString(), getSystemStyle(val, (SystemInterfaceFlags)2), (GUILayoutOption[])(object)new GUILayoutOption[0]);
					}
				}
				else if ((int)type != 8)
				{
					if ((int)type == 16)
					{
						EditorGUILayout.LabelField(val.get_systemName(), val.get_teardownDuration().ToString(), getSystemStyle(val, (SystemInterfaceFlags)16), (GUILayoutOption[])(object)new GUILayoutOption[0]);
					}
				}
				else
				{
					string text4 = $"Ø {val.get_averageCleanupDuration():00.000}".PadRight(12);
					string text5 = $"▼ {val.get_minCleanupDuration():00.000}".PadRight(12);
					string text6 = $"▲ {val.get_maxCleanupDuration():00.000}";
					EditorGUILayout.LabelField(val.get_systemName(), text4 + text5 + text6, getSystemStyle(val, (SystemInterfaceFlags)8), (GUILayoutOption[])(object)new GUILayoutOption[0]);
				}
				EditorGUILayout.EndHorizontal();
				num++;
			}
			ISystem system3 = val.get_system();
			DebugSystems val4 = (DebugSystems)(object)((system3 is DebugSystems) ? system3 : null);
			if (val4 != null)
			{
				int indentLevel2 = EditorGUI.get_indentLevel();
				EditorGUI.set_indentLevel(EditorGUI.get_indentLevel() + 1);
				num += drawSystemInfos(val4, type);
				EditorGUI.set_indentLevel(indentLevel2);
			}
		}
		return num;
	}

	private static SystemInfo[] getSortedSystemInfos(SystemInfo[] systemInfos, SortMethod sortMethod)
	{
		return sortMethod switch
		{
			SortMethod.Name => systemInfos.OrderBy((SystemInfo systemInfo) => systemInfo.get_systemName()).ToArray(), 
			SortMethod.NameDescending => systemInfos.OrderByDescending((SystemInfo systemInfo) => systemInfo.get_systemName()).ToArray(), 
			SortMethod.ExecutionTime => systemInfos.OrderBy((SystemInfo systemInfo) => systemInfo.get_averageExecutionDuration()).ToArray(), 
			SortMethod.ExecutionTimeDescending => systemInfos.OrderByDescending((SystemInfo systemInfo) => systemInfo.get_averageExecutionDuration()).ToArray(), 
			_ => systemInfos, 
		};
	}

	private static bool shouldShowSystems(DebugSystems systems, SystemInterfaceFlags type)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		if (!_hideEmptySystems)
		{
			return true;
		}
		if ((int)type <= 4)
		{
			if ((int)type == 2)
			{
				return systems.get_totalInitializeSystemsCount() > 0;
			}
			if ((int)type == 4)
			{
				return systems.get_totalExecuteSystemsCount() > 0;
			}
		}
		else
		{
			if ((int)type == 8)
			{
				return systems.get_totalCleanupSystemsCount() > 0;
			}
			if ((int)type == 16)
			{
				return systems.get_totalTearDownSystemsCount() > 0;
			}
		}
		return true;
	}

	private GUIStyle getSystemStyle(SystemInfo systemInfo, SystemInterfaceFlags systemFlag)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Invalid comparison between Unknown and I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Invalid comparison between Unknown and I4
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		GUIStyle val = new GUIStyle(GUI.get_skin().get_label());
		Color textColor = ((systemInfo.get_isReactiveSystems() && EditorGUIUtility.get_isProSkin()) ? Color.get_white() : val.get_normal().get_textColor());
		if ((int)systemFlag == 4 && systemInfo.get_averageExecutionDuration() >= (double)_systemWarningThreshold)
		{
			textColor = Color.get_red();
		}
		if ((int)systemFlag == 8 && systemInfo.get_averageCleanupDuration() >= (double)_systemWarningThreshold)
		{
			textColor = Color.get_red();
		}
		val.get_normal().set_textColor(textColor);
		return val;
	}

	private void addDuration(float duration)
	{
		if (Time.get_renderedFrameCount() != _lastRenderedFrameCount)
		{
			_lastRenderedFrameCount = Time.get_renderedFrameCount();
			if (_systemMonitorData.Count >= 60)
			{
				_systemMonitorData.Dequeue();
			}
			_systemMonitorData.Enqueue(duration);
		}
	}
}
