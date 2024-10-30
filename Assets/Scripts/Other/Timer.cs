using System;

public class Timer
{
    public float duration;
    public float leftTime;
    private Action updateAction;
    private Action callAction;
    
    public Timer(float duration, Action updateAction = null, Action callAction = null)
    {
        this.duration = duration;
        leftTime = duration;
        this.updateAction = updateAction;
        this.callAction = callAction;
    }

    public void OnUpdate(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime < 0)
        {
            leftTime = 0;
            callAction?.Invoke();
        }
        else
        {
            updateAction?.Invoke();
        }
    }

    public void Reset() => leftTime = duration;
}