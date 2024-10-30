using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    private Dictionary<string, Timer> timerDictionary;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        // 要在Awake初始化, 如果在Start初始化, 可能在Start之前调用方法导致未初始化错误
        timerDictionary = new Dictionary<string, Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in timerDictionary)
        {
            // Debug.Log("Timer:" + item.Key + " : " + item.Value.leftTime + " s");
            item.Value.OnUpdate(Time.deltaTime);
        }
    }

    public void AddTimer(string str, Timer timer)
    {
        if (timerDictionary.ContainsKey(str))
        {
            Debug.Log("[TimerManager]:" + str + " is already registered");
        }
        else
        {
            timerDictionary.Add(str, timer);
        }
    }

    public void RemoveTimer(string str)
    {
        Timer timer = timerDictionary[str];
        if (timer != null)
        {
            timerDictionary.Remove(str);
        }
    }

    public void ResetTimer(string str) => timerDictionary[str]?.Reset();
    
}
