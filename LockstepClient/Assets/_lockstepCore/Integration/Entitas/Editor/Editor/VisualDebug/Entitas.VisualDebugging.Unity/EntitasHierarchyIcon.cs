using System;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

[InitializeOnLoad]
public static class EntitasHierarchyIcon
{
	private static Texture2D _contextHierarchyIcon;

	private static Texture2D _contextErrorHierarchyIcon;

	private static Texture2D _entityHierarchyIcon;

	private static Texture2D _entityErrorHierarchyIcon;

	private static Texture2D _entityLinkHierarchyIcon;

	private static Texture2D _entityLinkWarnHierarchyIcon;

	private static Texture2D _systemsHierarchyIcon;

	private static Texture2D _systemsErrorHierarchyIcon;

	private static int _systemWarningThreshold;

	private static Texture2D contextHierarchyIcon
	{
		get
		{
			if ((Object)(object)_contextHierarchyIcon == (Object)null)
			{
				_contextHierarchyIcon = EditorLayout.LoadTexture("l:EntitasContextHierarchyIcon");
			}
			return _contextHierarchyIcon;
		}
	}

	private static Texture2D contextErrorHierarchyIcon
	{
		get
		{
			if ((Object)(object)_contextErrorHierarchyIcon == (Object)null)
			{
				_contextErrorHierarchyIcon = EditorLayout.LoadTexture("l:EntitasContextErrorHierarchyIcon");
			}
			return _contextErrorHierarchyIcon;
		}
	}

	private static Texture2D entityHierarchyIcon
	{
		get
		{
			if ((Object)(object)_entityHierarchyIcon == (Object)null)
			{
				_entityHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityHierarchyIcon");
			}
			return _entityHierarchyIcon;
		}
	}

	private static Texture2D entityErrorHierarchyIcon
	{
		get
		{
			if ((Object)(object)_entityErrorHierarchyIcon == (Object)null)
			{
				_entityErrorHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityErrorHierarchyIcon");
			}
			return _entityErrorHierarchyIcon;
		}
	}

	private static Texture2D entityLinkHierarchyIcon
	{
		get
		{
			if ((Object)(object)_entityLinkHierarchyIcon == (Object)null)
			{
				_entityLinkHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityLinkHierarchyIcon");
			}
			return _entityLinkHierarchyIcon;
		}
	}

	private static Texture2D entityLinkWarnHierarchyIcon
	{
		get
		{
			if ((Object)(object)_entityLinkWarnHierarchyIcon == (Object)null)
			{
				_entityLinkWarnHierarchyIcon = EditorLayout.LoadTexture("l:EntitasEntityLinkWarnHierarchyIcon");
			}
			return _entityLinkWarnHierarchyIcon;
		}
	}

	private static Texture2D systemsHierarchyIcon
	{
		get
		{
			if ((Object)(object)_systemsHierarchyIcon == (Object)null)
			{
				_systemsHierarchyIcon = EditorLayout.LoadTexture("l:EntitasSystemsHierarchyIcon");
			}
			return _systemsHierarchyIcon;
		}
	}

	private static Texture2D systemsErrorHierarchyIcon
	{
		get
		{
			if ((Object)(object)_systemsErrorHierarchyIcon == (Object)null)
			{
				_systemsErrorHierarchyIcon = EditorLayout.LoadTexture("l:EntitasSystemsErrorHierarchyIcon");
			}
			return _systemsErrorHierarchyIcon;
		}
	}

	static EntitasHierarchyIcon()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		try
		{
			_systemWarningThreshold = ConfigurableConfigExtension.CreateAndConfigure<VisualDebuggingConfig>(new Preferences("Entitas.properties", Environment.UserName + ".userproperties")).systemWarningThreshold;
		}
		catch (Exception)
		{
			_systemWarningThreshold = int.MaxValue;
		}
		EditorApplication.hierarchyWindowItemOnGUI = (HierarchyWindowItemCallback)Delegate.Combine((Delegate)(object)EditorApplication.hierarchyWindowItemOnGUI, (Delegate)new HierarchyWindowItemCallback(onHierarchyWindowItemOnGUI));
	}

	private static void onHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		Object obj = EditorUtility.InstanceIDToObject(instanceID);
		GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
		if (!((Object)(object)val != (Object)null))
		{
			return;
		}
		Rect val2 = default(Rect);
		((Rect)(ref val2))._002Ector(((Rect)(ref selectionRect)).get_x() + ((Rect)(ref selectionRect)).get_width() - 18f, ((Rect)(ref selectionRect)).get_y(), 16f, 16f);
		ContextObserverBehaviour component = val.GetComponent<ContextObserverBehaviour>();
		if ((Object)(object)component != (Object)null)
		{
			if (component.get_contextObserver().get_context().get_retainedEntitiesCount() != 0)
			{
				GUI.DrawTexture(val2, (Texture)(object)contextErrorHierarchyIcon);
			}
			else
			{
				GUI.DrawTexture(val2, (Texture)(object)contextHierarchyIcon);
			}
			return;
		}
		EntityBehaviour component2 = val.GetComponent<EntityBehaviour>();
		if ((Object)(object)component2 != (Object)null)
		{
			if (component2.get_entity().get_isEnabled())
			{
				GUI.DrawTexture(val2, (Texture)(object)entityHierarchyIcon);
			}
			else
			{
				GUI.DrawTexture(val2, (Texture)(object)entityErrorHierarchyIcon);
			}
			return;
		}
		EntityLink component3 = val.GetComponent<EntityLink>();
		if ((Object)(object)component3 != (Object)null)
		{
			if (component3.get_entity() != null)
			{
				GUI.DrawTexture(val2, (Texture)(object)entityLinkHierarchyIcon);
			}
			else
			{
				GUI.DrawTexture(val2, (Texture)(object)entityLinkWarnHierarchyIcon);
			}
			return;
		}
		DebugSystemsBehaviour component4 = val.GetComponent<DebugSystemsBehaviour>();
		if ((Object)(object)component4 != (Object)null)
		{
			if (component4.get_systems().get_executeDuration() < (double)_systemWarningThreshold)
			{
				GUI.DrawTexture(val2, (Texture)(object)systemsHierarchyIcon);
			}
			else
			{
				GUI.DrawTexture(val2, (Texture)(object)systemsErrorHierarchyIcon);
			}
		}
	}
}
