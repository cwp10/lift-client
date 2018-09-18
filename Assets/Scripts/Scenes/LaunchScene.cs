using UnityEngine;

public class LaunchScene : SceneDirector.Scene
{
    public override void OnAwakeScene()
    {

    }

    public override void OnStartScene()
    {
        SceneDirector.Instance.Next("Lobby");
    }
}
