using System;
using System.Linq;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using Entitas.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.Migration.Unity.Editor;

public class MigrationWindow : EditorWindow
{
	private Texture2D _headerTexture;

	private IMigration[] _migrations;

	private bool[] _showMigration;

	private Vector2 _scrollViewPosition;

	[MenuItem("Tools/Entitas/Migrate...", false, 1000)]
	public static void OpenMigrate()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		MigrationWindow window = EditorWindow.GetWindow<MigrationWindow>(true, "Entitas Migration - " + CheckForUpdates.GetLocalVersion());
		((EditorWindow)window).set_minSize(new Vector2(415f, 564f));
		((EditorWindow)window).Show();
	}

	private void OnEnable()
	{
		_headerTexture = EditorLayout.LoadTexture("l:EntitasHeader");
		_migrations = getMigrations();
		_showMigration = new bool[_migrations.Length];
		_showMigration[0] = true;
	}

	private static IMigration[] getMigrations()
	{
		return (from instance in AppDomainExtension.GetInstancesOf<IMigration>(AppDomain.CurrentDomain)
			orderby instance.GetType().FullName descending
			select instance).ToArray();
	}

	private void OnGUI()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_scrollViewPosition = EditorGUILayout.BeginScrollView(_scrollViewPosition, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorLayout.DrawTexture(_headerTexture);
		GUIStyle val = new GUIStyle(GUI.get_skin().get_label());
		val.set_wordWrap(true);
		for (int i = 0; i < _migrations.Length; i++)
		{
			IMigration migration = _migrations[i];
			_showMigration[i] = EditorLayout.DrawSectionHeaderToggle(migration.version, _showMigration[i]);
			if (_showMigration[i])
			{
				EditorLayout.BeginSectionContent();
				EditorGUILayout.LabelField(migration.description, val, (GUILayoutOption[])(object)new GUILayoutOption[0]);
				if (GUILayout.Button("Apply migration " + migration.version, (GUILayoutOption[])(object)new GUILayoutOption[0]))
				{
					migrate(migration, this);
				}
				EditorLayout.EndSectionContent();
			}
		}
		EditorGUILayout.EndScrollView();
	}

	private static void migrate(IMigration migration, MigrationWindow window)
	{
		if (EditorUtility.DisplayDialog("Migrate", "You are about to migrate your source files. Make sure that you have committed your current project or that you have a backup of your project before you proceed.", "I have a backup - Migrate", "Cancel"))
		{
			((EditorWindow)window).Close();
			EditorUtility.DisplayDialog("Migrate", "Please select the folder, " + migration.workingDirectory + ".", "I will select the requested folder");
			string text = "Assets/";
			text = EditorUtility.OpenFolderPanel(migration.version + ": " + migration.workingDirectory, text, string.Empty);
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("Could not complete migration! Selected path was invalid!");
			}
			MigrationFile[] array = migration.Migrate(text);
			Debug.Log((object)("Applying " + migration.version));
			MigrationFile[] array2 = array;
			foreach (MigrationFile migrationFile in array2)
			{
				MigrationUtils.WriteFiles(array);
				Debug.Log((object)("Migrated " + migrationFile.fileName));
			}
		}
	}
}
