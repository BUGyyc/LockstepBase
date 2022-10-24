using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

[CustomEditor(typeof(EntityBehaviour))]
[CanEditMultipleObjects]
public class EntityInspector : Editor
{
	public override void OnInspectorGUI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (((Editor)this).get_targets().Length == 1)
		{
			EntityDrawer.DrawEntity(((EntityBehaviour)((Editor)this).get_target()).get_entity());
		}
		else
		{
			EntityDrawer.DrawMultipleEntities((from t in ((Editor)this).get_targets()
				select ((EntityBehaviour)t).get_entity()).ToArray());
		}
		if (((Editor)this).get_target() != (Object)null)
		{
			EditorUtility.SetDirty(((Editor)this).get_target());
		}
	}
}
