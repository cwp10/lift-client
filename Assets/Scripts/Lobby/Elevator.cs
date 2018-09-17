using UnityEngine;

public class Elevator : MonoBehaviour
{
	[SerializeField] private GameObject subMenuButtons_ = null;
	
	private LobbyScene _scene = null;

	private void OnEnable()
	{
		_scene = SceneDirector.Instance.CurrentScene.Casting<LobbyScene>();
        _scene.LobbyEvent.Register(EventType.Lobby.BEGIN_ELEVATOR, OnDiaableSubMenu);
		_scene.LobbyEvent.Register(EventType.Lobby.END_ELEVATOR, OnEnableSubMenu);
	}

	private void OnDisable()
	{
        _scene.LobbyEvent.UnRegister(EventType.Lobby.BEGIN_ELEVATOR, OnDiaableSubMenu);
		_scene.LobbyEvent.UnRegister(EventType.Lobby.END_ELEVATOR, OnEnableSubMenu);
	}

	private void OnDiaableSubMenu(object sender, object[] args)
	{
		subMenuButtons_.SetActive(false);
	}

	private void OnEnableSubMenu(object sender, object[] args)
	{
		int subIndex = (int)args[0];
		Debug.Log(subIndex);
		subMenuButtons_.SetActive(true);
	}
}
