using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.Editor;

public static class EntitasFeedback
{
	[MenuItem("Tools/Entitas/Documentation...", false, 11)]
	public static void EntitasDocs()
	{
		Application.OpenURL("http://sschmid.github.io/Entitas-CSharp/");
	}

	[MenuItem("Tools/Entitas/Feedback/Report a bug...", false, 20)]
	public static void ReportBug()
	{
		Application.OpenURL("https://github.com/sschmid/Entitas-CSharp/issues");
	}

	[MenuItem("Tools/Entitas/Feedback/Request a feature...", false, 21)]
	public static void RequestFeature()
	{
		Application.OpenURL("https://github.com/sschmid/Entitas-CSharp/issues");
	}

	[MenuItem("Tools/Entitas/Feedback/Join the Entitas chat...", false, 22)]
	public static void EntitasChat()
	{
		Application.OpenURL("https://gitter.im/sschmid/Entitas-CSharp");
	}

	[MenuItem("Tools/Entitas/Feedback/Entitas Wiki...", false, 23)]
	public static void EntitasWiki()
	{
		Application.OpenURL("https://github.com/sschmid/Entitas-CSharp/wiki");
	}

	[MenuItem("Tools/Entitas/Feedback/Donate...", false, 24)]
	public static void Donate()
	{
		Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=BTMLSDQULZ852");
	}
}
