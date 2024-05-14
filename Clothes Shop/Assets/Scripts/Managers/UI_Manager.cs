using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour,IPauseable
{
    [SerializeField] private GameObject _menuPanel;

    private void Awake()
    {
        _menuPanel.SetActive(false);
    }

    private void Start()
    {
        ScreenManager.Instance.AddPauseable(this);
    }

    public void Pause(bool isDialoguePause = false)
    {
        if(isDialoguePause)return;
        _menuPanel.SetActive(true);
    }

    public void Resume()
    {
        _menuPanel.SetActive(false);
    }

    private void OnDisable()
    {
        ScreenManager.Instance.RemovePauseable(this);
    }
}
