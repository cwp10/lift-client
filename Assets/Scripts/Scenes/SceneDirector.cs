using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : UnitySingleton<SceneDirector>
{
    /// <summary>
    /// SceneObject
    /// </summary>
    /// <para>모든 씬은 해당 클래스를 상속받아야 한다.</para>
    /// <para>상속받은 클래스에서 MonoBehaviour 클래스의 메소드를 직접 재정의하지 말자.</para>
    public abstract class Scene : UnityBehaviour
    {
        public virtual void OnAwakeScene() { }
        public virtual void OnStartScene() { }
        public virtual void OnUpdateScene() { }
        public virtual void OnDestroyScene() { }

        private void Awake()
        {
            SceneDirector.Instance.CurrentScene = this;
            this.OnAwakeScene();
        }

        private void Start() { this.OnStartScene(); }
        private void Update() { this.OnUpdateScene(); }
        private void OnDestroy() { this.OnDestroyScene(); }

        public T Casting<T>() where T : Scene
        {
            Debug.Assert(this is T);
            return this as T;
        }

        public void Next(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public Scene CurrentScene
    {
        get;
        private set;
    }

    public GlobalEventHandler GlobalEvent
    {
        get;
        private set;
    }

    public void Next(string sceneName)
    {
        CurrentScene.Next(sceneName);
    }

    protected override void Awake()
    {
        base.Awake();
        this.GlobalEvent = new GlobalEventHandler();
    }

    protected void Update()
    {
        this.GlobalEvent.Produceed();
    }
}
