using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenManager : SingeltonMonoBehaviour<ScreenManager>
{
    public bool isPaused;
    private List<IPauseable> _pauseables = new List<IPauseable>();
    public void AddPauseable(IPauseable pauseable) => _pauseables.Add(pauseable);
    public void RemovePauseable(IPauseable pauseable) => _pauseables.Remove(pauseable);

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (isPaused)
            {
                case false:
                    Pause();
                    return;
                default:
                    Resume();
                    return;
            }
        }
    }

    public void Pause(bool isDialoguePause = false)
    {
        isPaused = true;
        foreach (var pauseable in _pauseables)    
        {
            pauseable.Pause(isDialoguePause);
        }
    }

    public void Resume()
    {
        isPaused = false; 
        foreach (var pauseable in _pauseables)
        {
            pauseable.Resume();
        }
    }
}
