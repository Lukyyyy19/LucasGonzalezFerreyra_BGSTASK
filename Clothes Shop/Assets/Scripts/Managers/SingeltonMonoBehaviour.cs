using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingeltonMonoBehaviour<T> : MonoBehaviour where T : SingeltonMonoBehaviour<T>
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (!Instance)
        {
            Instance = (T)this;
        }
    }
}
