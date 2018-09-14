using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인스턴스로 생성된 오브젝트를 풀에 담아 재사용하는 클래스
/// </summary>
public class GameObjectPool : UnitySingleton<GameObjectPool>
{
    class Pool
    {
        private GameObject _parent = null;
        private GameObject _prefab = null;
        private List<GameObject> _gameObjectList = null;

        public Pool(GameObject prefab, GameObject parent, int count)
        {
            _prefab = prefab;
            _parent = parent;

            _gameObjectList = new List<GameObject>();

            for (int i = 0; i < count; ++i)
            {
                GameObject go = CreateObject();
                go.SetActive(false);
                _gameObjectList.Add(go);
            }
        }

        public GameObject GetObject()
        {
            for (int i = 0; i < _gameObjectList.Count; i++)
            {
                if (!_gameObjectList[i].activeSelf)
                {
                    _gameObjectList[i].SetActive(true);
                    return _gameObjectList[i];
                }
            }
            return CreateObject();
        }

        public void Destroy()
        {
            for (int i = 0; i < _gameObjectList.Count; i++)
            {
                GameObject.Destroy(_gameObjectList[i]);
            }
            _gameObjectList = null;
            _prefab = null;
            _parent = null;
        }

        private GameObject CreateObject()
        {
            GameObject go = GameObject.Instantiate(_prefab) as GameObject;
            if (_parent != null)
            {
                go.transform.SetParent(_parent.transform);
            }
            _gameObjectList.Add(go);
            return go;
        }
    }

    private Dictionary<GameObject, Pool> _poolDic = new Dictionary<GameObject, Pool>();

    /// <summary>
    /// 오브젝트를 생성한다
    /// </summary>
    /// <param name="prefab">인스턴스화할 프리팹 객체</param>
    /// <param name="count">인스턴스 갯수</param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, int count = 1)
    {
        AddObject(prefab, count);
        return _poolDic[prefab].GetObject();
    }

    /// <summary>
    /// 오브젝트를 생성한다
    /// </summary>
    /// <param name="prefab">인스턴스화할 프리팹 객체</param>
    /// <param name="position">인스턴스의 포지션</param>
    /// <param name="rotation">인스턴스의 로테이션</param>
    /// <param name="count">인스턴스 갯수</param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, int count = 1)
    {
        GameObject obj = Spawn(prefab, count);
        obj.transform.SetParent(null);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    /// <summary>
    /// 오브젝트를 제거한다
    /// </summary>
    /// <param name="go">제거할 대상</param>
    public void Despawn(GameObject go)
    {
        if (go != null)
        {
            go.transform.SetParent(null);
            go.transform.SetParent(Instance.gameObject.transform);
            go.SetActive(false);
        }
    }

    /// <summary>
    /// 시간차를 두고 오브젝트를 제거한다.
    /// </summary>
    /// <param name="go">제거할 대상</param>
    /// <param name="time">대상이 사라지는 시간</param>
    public void DelayDespawn(GameObject go, float time)
    {
        StartCoroutine(DespawnTimer(go, time));
    }

    private void AddObject(GameObject prefab, int count = 1)
    {
        if (!_poolDic.ContainsKey(prefab))
        {
            _poolDic.Add(prefab, new Pool(prefab, Instance.gameObject, count));
        }
    }

    private IEnumerator DespawnTimer(GameObject prefab, float time)
    {
        yield return new WaitForSeconds(time);
        Despawn(prefab);
    }
}
