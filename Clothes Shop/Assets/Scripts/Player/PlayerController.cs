using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Player
{
    
[Serializable]
public class PlayerController
{
    private PlayerView _view;
    [SerializeField]private PlayerData _playerData;
    private bool _isPaused;
    public PlayerController(PlayerView view)
    {
        _view = view;
        _playerData = new PlayerData();
        _playerData.ChangeCloth(_view._startCloth);
        _playerData.OnDirectionChanged += _view.ChangeAnimation;
    }

    public void Move()
    {
        if (_isPaused)
        {
            _playerData.UpdateDirection(0,0);
            return;
        }
        _playerData.UpdateDirection(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        var translation = _playerData.Dir * _playerData.Speed;
        _view.Rb.velocity = translation;
    }

    public WalkDirection GetCurrentDirection()
    {
        return _playerData.CurrentDirection;
    }

    public Clothes GetCurrentCloth()
    {
        return _playerData.CurrentCloth;
    }

    public void OnDestroy()
    {
        _playerData.OnDirectionChanged -= _view.ChangeAnimation;
    }

    public void Pause()
    {
        _isPaused = true;
        _playerData.Pause();
    }

    public void Resume()
    {
        _isPaused = false;
        _playerData.Resume();
    }
}
}
