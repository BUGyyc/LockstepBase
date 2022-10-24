using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Entitas.Unity.Editor;

public static class CheckForUpdates
{
	private struct ResponseData
	{
		public string tag_name;
	}

	private const string URL_GITHUB_API_LATEST_RELEASE = "https://api.github.com/repos/sschmid/Entitas-CSharp/releases/latest";

	private const string URL_GITHUB_RELEASES = "https://github.com/sschmid/Entitas-CSharp/releases";

	private const string URL_ASSET_STORE = "http://u3d.as/NuJ";

	[MenuItem("Tools/Entitas/Check for Updates...", false, 10)]
	public static void DisplayUpdates()
	{
		displayUpdateInfo(GetUpdateInfo());
	}

	public static UpdateInfo GetUpdateInfo()
	{
		string localVersion = GetLocalVersion();
		string remoteVersion = GetRemoteVersion();
		return new UpdateInfo(localVersion, remoteVersion);
	}

	public static string GetLocalVersion()
	{
		return EntitasResources.GetVersion();
	}

	public static string GetRemoteVersion()
	{
		try
		{
			return JsonUtility.FromJson<ResponseData>(requestLatestRelease()).tag_name;
		}
		catch (Exception)
		{
		}
		return string.Empty;
	}

	private static string requestLatestRelease()
	{
		string empty = string.Empty;
		UnityWebRequest val = UnityWebRequest.Get("https://api.github.com/repos/sschmid/Entitas-CSharp/releases/latest");
		try
		{
			UnityWebRequestAsyncOperation val2 = val.SendWebRequest();
			while (!((AsyncOperation)val2).get_isDone())
			{
			}
			if (!val.get_isNetworkError())
			{
				if (!val.get_isHttpError())
				{
					return val2.get_webRequest().get_downloadHandler().get_text();
				}
				return empty;
			}
			return empty;
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private static void displayUpdateInfo(UpdateInfo info)
	{
		switch (info.updateState)
		{
		case UpdateState.UpdateAvailable:
			if (EditorUtility.DisplayDialog("Entitas Update", $"A newer version of Entitas is available!\n\nCurrently installed version: {info.localVersionString}\nNew version: {info.remoteVersionString}", "Show in Unity Asset Store", "Cancel"))
			{
				Application.OpenURL("http://u3d.as/NuJ");
			}
			break;
		case UpdateState.UpToDate:
			EditorUtility.DisplayDialog("Entitas Update", "Entitas is up to date (" + info.localVersionString + ")", "Ok");
			break;
		case UpdateState.AheadOfLatestRelease:
			if (EditorUtility.DisplayDialog("Entitas Update", $"Your Entitas version seems to be newer than the latest release?!?\n\nCurrently installed version: {info.localVersionString}\nLatest release: {info.remoteVersionString}", "Show in Unity Asset Store", "Cancel"))
			{
				Application.OpenURL("http://u3d.as/NuJ");
			}
			break;
		case UpdateState.NoConnection:
			if (EditorUtility.DisplayDialog("Entitas Update", "Could not request latest Entitas version!\n\nMake sure that you are connected to the internet.\n", "Try again", "Cancel"))
			{
				DisplayUpdates();
			}
			break;
		}
	}
}
