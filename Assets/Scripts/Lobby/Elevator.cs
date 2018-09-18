using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour
{
	[SerializeField] private GameObject subMenuButtons_ = null;
	[SerializeField] private Animator doorAnimator_ = null;
	[SerializeField] private Transform subMenuTransform_ = null;
	private List<GameObject> _subMenuList = new List<GameObject>();
	private LobbyScene _scene = null;

	private void Awake()
	{
		for (int i = 0; i < subMenuTransform_.childCount; i++)
		{
			_subMenuList.Add(subMenuTransform_.GetChild(i).gameObject);
		}
	}

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
		doorAnimator_.SetBool("isOpen", false);
	}

	private void OnEnableSubMenu(object sender, object[] args)
	{
		int subIndex = (int)args[0];
		
		for (int i = 0; i < _subMenuList.Count; i++)
		{
			if(i == subIndex) _subMenuList[i].SetActive(true);
			else _subMenuList[i].SetActive(false);
		}
		
		subMenuButtons_.SetActive(true);
		doorAnimator_.SetBool("isOpen", true);
	}
}
