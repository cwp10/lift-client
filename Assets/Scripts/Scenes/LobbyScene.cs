using UnityEngine;

public class LobbyScene : SceneDirector.Scene
{
	public readonly LobbyEventHandler LobbyEvent = new LobbyEventHandler();

	public override void OnAwakeScene()
	{
		
	}

	public override void OnUpdateScene()
	{
		LobbyEvent.Produceed();
	}
}
