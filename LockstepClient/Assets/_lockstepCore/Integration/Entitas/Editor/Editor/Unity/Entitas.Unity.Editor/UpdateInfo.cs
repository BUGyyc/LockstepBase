using System;

namespace Entitas.Unity.Editor;

public class UpdateInfo
{
	public readonly string localVersionString;

	public readonly string remoteVersionString;

	private readonly UpdateState _updateState;

	public UpdateState updateState => _updateState;

	public UpdateInfo(string localVersionString, string remoteVersionString)
	{
		this.localVersionString = localVersionString.Trim();
		this.remoteVersionString = remoteVersionString.Trim();
		if (remoteVersionString != string.Empty)
		{
			Version value = new Version(localVersionString);
			switch (new Version(remoteVersionString).CompareTo(value))
			{
			case 1:
				_updateState = UpdateState.UpdateAvailable;
				break;
			case 0:
				_updateState = UpdateState.UpToDate;
				break;
			case -1:
				_updateState = UpdateState.AheadOfLatestRelease;
				break;
			}
		}
		else
		{
			_updateState = UpdateState.NoConnection;
		}
	}
}
