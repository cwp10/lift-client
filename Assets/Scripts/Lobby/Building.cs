using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class Building : MonoBehaviour
{
    private ScrollRect _scrollRect = null;
    private RectTransform _rectTransform = null;
    private GridLayoutGroup _gridLayoutGrop = null;   
    private Vector2 _destPosition = Vector2.zero;
    private Vector2 _initialPosition = Vector2.zero;
    private AnimationCurve _animationCurve = null;
    private Rect _currentViewRect = new Rect();
    private LobbyScene _scene = null;
    private bool _isAnimating = false;
    private int _prevfloorIndex = 0;

    private void Awake()
    {
        _scrollRect = this.GetComponent<ScrollRect>();
        _rectTransform = this.GetComponent<RectTransform>();
        _gridLayoutGrop = _scrollRect.content.GetComponent<GridLayoutGroup>();
    }

    private void OnEnable()
    {
        _scene = SceneDirector.Instance.CurrentScene.Casting<LobbyScene>();
        _scene.LobbyEvent.Register(EventType.Lobby.MOVE_ELEVATOR, OnElevatorFloorIndex);
    }

    private void OnDisable()
    {
        _scene.LobbyEvent.UnRegister(EventType.Lobby.MOVE_ELEVATOR, OnElevatorFloorIndex);
    }

    private void Start()
    {
        UpdateView();
        StartCoroutine(MoveElevator(_gridLayoutGrop.transform.childCount - 1, 0));
    }

    private void Update()
    {
        if (_rectTransform.rect.width != _currentViewRect.width || _rectTransform.rect.height != _currentViewRect.height)
        {
            UpdateView();
        }
    }
    
    private void LateUpdate()
    {
        if(!_isAnimating) return;

        if (Time.time >= _animationCurve.keys[_animationCurve.length - 1].time)
        {
            _scrollRect.content.anchoredPosition = _destPosition;
            _isAnimating = false;
            _scene.LobbyEvent.Send(EventType.Lobby.END_ELEVATOR, this, _prevfloorIndex);
            return;
        }

        Vector2 newPoisition = _initialPosition + (_destPosition - _initialPosition) * _animationCurve.Evaluate(Time.time);
        _scrollRect.content.anchoredPosition = newPoisition;
    }

    private void UpdateView()
    {
        _currentViewRect = _rectTransform.rect;

        int paddingH = Mathf.RoundToInt((_currentViewRect.width - _gridLayoutGrop.cellSize.x) / 2.0f);
        int paddingV = Mathf.RoundToInt((_currentViewRect.height - _gridLayoutGrop.cellSize.y) / 2.0f);
        _gridLayoutGrop.padding = new RectOffset(paddingH, paddingH, paddingV, paddingV);
    }

    private void UpdateElevator(int floorIndex, float time)
    {   
        float height = _gridLayoutGrop.cellSize.y + _gridLayoutGrop.spacing.y;
        float destY = floorIndex * height;

        _destPosition = new Vector2(_scrollRect.content.anchoredPosition.x, destY);
        _initialPosition = _scrollRect.content.anchoredPosition;
        _scrollRect.StopMovement();

        Keyframe keyFrame1 = new Keyframe(Time.time, 0.0f, 0.0f, 1.0f);
		Keyframe keyFrame2 = new Keyframe(Time.time + time, 1.0f, 0.0f, 0.0f);
		_animationCurve = new AnimationCurve(keyFrame1, keyFrame2);

        _prevfloorIndex = floorIndex;
        _isAnimating = true;
    }

    private void OnElevatorFloorIndex(object sender, object[] args)
    {
        int index = (int)args[0];
        float time = Mathf.Abs(_prevfloorIndex - index) * 0.2f;
        if(_prevfloorIndex != index)
        {
            StartCoroutine(MoveElevator(index, time));
        }
    }

    private IEnumerator MoveElevator(int floorIndex, float time)
    {
        if(_isAnimating) yield break;

        _scene.LobbyEvent.Send(EventType.Lobby.BEGIN_ELEVATOR, this);
        yield return new WaitForSeconds(0.3f);
        
        UpdateElevator(floorIndex, time);
    }
}
