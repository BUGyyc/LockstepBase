using System.Linq;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

[CustomEditor(typeof(EntityLink))]
public class EntityLinkInspector : Editor
{
	public override void OnInspectorGUI()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		EntityLink link = (EntityLink)((Editor)this).get_target();
		if (link.get_entity() != null && GUILayout.Button("Unlink", (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			link.Unlink();
		}
		if (link.get_entity() != null)
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField(((object)link.get_entity()).ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			if (GUILayout.Button("Show entity", (GUILayoutOption[])(object)new GUILayoutOption[0]))
			{
				Selection.set_activeGameObject(((Component)Object.FindObjectsOfType<EntityBehaviour>().Single((EntityBehaviour e) => e.get_entity() == link.get_entity())).get_gameObject());
			}
			EditorGUILayout.Space();
			EntityDrawer.DrawEntity(link.get_entity());
		}
		else
		{
			EditorGUILayout.LabelField("Not linked to an entity", (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
	}
}
