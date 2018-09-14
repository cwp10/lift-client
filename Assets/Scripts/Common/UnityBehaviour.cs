using UnityEngine;

public abstract class UnityBehaviour : MonoBehaviour
{
    public void SetParent(MonoBehaviour parent)
    {
        this.transform.SetParent(parent.transform);
    }

    public void SetParent(Transform parent)
    {
        this.transform.SetParent(parent);
    }

    public void AddChild(MonoBehaviour child)
    {
        child.transform.SetParent(this.transform);
    }

    public void AddChild(Transform child)
    {
        child.SetParent(this.transform);
    }
}
