using UnityEngine;
using System.IO;

public static class JsonFile
{
    public enum Repository
    {
        StreamingAssets = 0,
        PersistentData,
    }

    /// <summary>
    /// JSON파일을 불러와 객체를 생성한다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">Json파일의 경로 (가급적 확장자 생략)</param>
    /// <param name="repo">파일이 위치한 저장소</param>
    /// <returns>JsonObject, 실패시 null</returns>
    public static T Load<T>(string path, Repository repo = Repository.StreamingAssets) where T : JsonObject
    {
        string fullpath = MakeFilePath(path, repo);
        if (!File.Exists(fullpath))
        {
            return null;
        }

        string data = string.Empty;
        using (StreamReader reader = new StreamReader(fullpath))
        {
            data = reader.ReadToEnd();
            reader.Close();
        }
        return JsonObject.Parse<T>(data);
    }

    /// <summary>
    /// Object를 JSON파일로 생성 한다.
    /// </summary>
    /// <param name="obj">Json으로 만들 오브젝트</param>
    /// <param name="path">Json파일의 경로 (가급적 확장자 생략)</param>
    /// <param name="repo">파일이 위치한 저장소</param>
    public static void Write(object obj, string path, Repository repo = Repository.StreamingAssets)
    {
        string fullpath = MakeFilePath(path, repo);

        CreateDirectory(fullpath);

        using (StreamWriter writer = new StreamWriter(fullpath))
        {
            writer.Write(JsonHelper.ToJson(obj));
            writer.Close();
        }
    }

    private static string MakeFilePath(string path, Repository repo)
    {
        string basePath = Application.streamingAssetsPath;
        if (repo == Repository.PersistentData)
        {
            basePath = Application.persistentDataPath;
        }
        string fullpath = Path.Combine(basePath, FilterExt(path));
        fullpath = fullpath.Replace(@"\", "/");
        return fullpath;
    }

    private static string FilterExt(string filename)
    {
        string ext = Path.GetExtension(filename);
        if (string.IsNullOrEmpty(ext))
        {
            return filename + ".json";
        }

        return filename;
    }

    private static void CreateDirectory(string path)
    {
        string[] splits = path.Split('/');
        string directoryPath = string.Empty;

        for (int i = 0; i < splits.Length - 1; i++)
        {
            directoryPath += splits[i] + Path.DirectorySeparatorChar;
        }

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}
