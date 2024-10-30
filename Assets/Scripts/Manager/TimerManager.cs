using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    private List<Timer> timers;
    private Dictionary<string, Timer> timerDictionary;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        // 要在Awake初始化, 如果在Start初始化, 可能在Start之前调用方法导致未初始化错误
        timers = new List<Timer>();
        timerDictionary = new Dictionary<string, Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Timer timer in timers)
        {
            timer.OnUpdate(Time.deltaTime);
        }
    }

    public void AddTimer(string str, Timer timer)
    {
        // 重置时间
        if (timerDictionary.ContainsKey(str))
        {
            timerDictionary[str].Reset();
        }
        else
        {
            timerDictionary.Add(str, timer);
            timers.Add(timer);
        }
    }

    public void RemoveTimer(string str)
    {
        Timer timer = timerDictionary[str];
        if (timer != null)
        {
            timers.Remove(timer);
            timerDictionary.Remove(str);
        }
    }

    public void ResetTimer(string str) => timerDictionary[str]?.Reset();
}
