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
    public PlayerController(PlayerView view)
    {
        _view = view;
        _playerData = new PlayerData();
        _playerData.ChangeCloth(_view._startCloth);
        _playerData.OnDirectionChanged += _view.ChangeAnimation;
    }

    public void Move()
    {
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
}
}
