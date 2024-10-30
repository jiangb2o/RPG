using System;

public class Timer
{
    public float duration;
    public float leftTime;
    public bool repeat;
    private Action updateAction;
    private Action callAction;
    
    public Timer(float duration, bool repeat = true, Action updateAction = null, Action callAction = null)
    {
        this.duration = duration;
        leftTime = duration;
        this.repeat = repeat;
        this.updateAction = updateAction;
        this.callAction = callAction;
    }

    public void OnUpdate(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime < 0)
        {
            callAction?.Invoke();
            if(repeat) Reset();
        }
        else
        {
            updateAction?.Invoke();
        }
    }

    public void Reset() => leftTime = duration;
}