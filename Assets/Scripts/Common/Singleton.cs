/// <summary>
/// 싱글턴 추상클래스
/// </summary>
/// <para>외부에서는 Instance, 내부에서는 self 프로퍼티를 통해 접근한다.</para>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T _instance = default(T);

    public static T Instance
    {
        get
        {
            if (_instance == default(T))
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    protected static T self { get { return _instance; } }
}
