using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TimerManager {

    #region
    private static TimerManager instance;

    private TimerManager() { }

    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimerManager();
            }
            return instance;
        }
    }
    #endregion Singleton

    public delegate void NonParamDelg();

    List<Timer> timers;
    Stack<Timer> timersToRemove;
    Stack<Timer> timersToAdd;

    public void Initialize()
    {
        timers = new List<Timer>();
        timersToRemove = new Stack<Timer>();

    }

    public void Update(float dt)
    {
        while(timersToRemove.Count > 0)
            timers.Remove(timersToRemove.Pop());
        while (timersToAdd.Count > 0)
            timers.Add(timersToAdd.Pop());

        foreach (Timer t in timers)
        {
            t.countdown -= dt;
            if (t.countdown <= 0)
            {
                t.InvokeTimerFunc();
            }
        }
    }

    //A timer trigging can remove other timers, so need to be safe
    public void RemoveTimer(Timer t)
    {
        timersToRemove.Push(t);
    }

    public class Timer
    {
        public float countdown;
        NonParamDelg funcToInvoke;
        //object[] args;

        public Timer(float t, NonParamDelg genericDelegate)
        {
            countdown = t;
            funcToInvoke = genericDelegate;
        }

        public void InvokeTimerFunc()
        {
            funcToInvoke.Invoke();
        }
    }
        
}
