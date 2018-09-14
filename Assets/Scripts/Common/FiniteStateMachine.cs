using System;
using System.Collections.Generic;

public class FiniteStateMachine<T>
{
    public T State { get; private set; }
    public Action<T> OnEnter { get; set; }
    public Action<T> OnLeave { get; set; }

    private Dictionary<T, Action> _stateActions = new Dictionary<T, Action>();
    private Queue<T> _stateQueue = new Queue<T>();

    public FiniteStateMachine()
    {
        this.OnEnter = (T t) => { };
        this.OnLeave = (T t) => { };
    }

    /// <summary>
    /// State를 즉시 변경한다. 대부분의 경우에는 AddState를 사용
    /// </summary>
    /// <param name="state">변경하고 싶은 상태</param>
    public void SetState(T state)
    {
        if (this.State.Equals(state)) return;
        OnLeave(this.State);
        this.State = state;
        OnEnter(this.State);
    }

    /// <summary>
    /// State를 큐에 추가한다. State가 즉시 변경되지 않는다.
    /// </summary>
    /// <param name="state">변경하고 싶은 상태</param>
    public void AddState(T state)
    {
        this._stateQueue.Enqueue(state);
    }

    public void SetAction(T state, Action action)
    {
        this._stateActions[state] = action;
    }

    public void Update()
    {
        if (0 < this._stateQueue.Count)
        {
            OnLeave(this.State);
            SetState(this._stateQueue.Dequeue());
        }
        this._stateActions[this.State]();
    }
}
