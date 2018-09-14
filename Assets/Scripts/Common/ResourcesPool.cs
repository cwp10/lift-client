using System.Collections.Generic;
using UnityEngine;

public static class ResourcesPool
{
    private static Dictionary<string, GameObject> _loadedPrefabsGo = new Dictionary<string, GameObject>();
    private static Dictionary<string, Object> _loadedPrefabs = new Dictionary<string, Object>();

    public static GameObject Load(string path)
    {
        if (!_loadedPrefabsGo.ContainsKey(path))
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.Assert(false, "prefab load error. plz input right path : " + path);
                return null;
            }

            _loadedPrefabsGo.Add(path, prefab);
        }

        GameObject result = _loadedPrefabsGo[path];

        return result;
    }

    public static T Load<T>(string path) where T : Object
    {
        if (!_loadedPrefabs.ContainsKey(path))
        {
            T prefab = Resources.Load<T>(path);
            if (prefab == null)
            {
                Debug.Assert(false, "prefab load error. plz input right path : " + path);
                return null;
            }

            _loadedPrefabs.Add(path, prefab);
        }

        T result = _loadedPrefabs[path] as T;

        return result;
    }

    public static void Unload(string path)
    {
        if (_loadedPrefabsGo.ContainsKey(path))
        {
            GameObject r = _loadedPrefabsGo[path];
            _loadedPrefabsGo.Remove(path);

            Resources.UnloadAsset(r);
        }

        if (_loadedPrefabs.ContainsKey(path))
        {
            Object r = _loadedPrefabs[path];
            _loadedPrefabs.Remove(path);

            Resources.UnloadAsset(r);
        }
    }

    public static void Clear()
    {
        _loadedPrefabsGo.Clear();
        _loadedPrefabs.Clear();

        Resources.UnloadUnusedAssets();
    }
}
