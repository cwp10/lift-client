using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform contentTrans_ = null;
    [SerializeField] private RawImage rawImage_ = null;
    private float _contentMaxYPos = 0.0f;

    private void Start()
    {
        int count = contentTrans_.childCount;
        _contentMaxYPos = contentTrans_.GetComponent<GridLayoutGroup>().cellSize.y * count;
    }

    private void Update()
    {
        float uvPoint = 1.0f - (contentTrans_.position.y / _contentMaxYPos);
        rawImage_.uvRect = new Rect(0, uvPoint, 1, 1);
    }
}
