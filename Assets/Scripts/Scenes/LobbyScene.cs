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

	public void OnClickElevatorButton(int index)
	{
		this.LobbyEvent.Send(EventType.Lobby.MOVE_ELEVATOR, this, index);
	}
}
