using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.Collections;

namespace Player
{
    
[Serializable]
public class PlayerData
{
    //Movement
    private float _speed = 5;
    public float Speed => _speed;
    private Vector2 _direction;
    public Vector2 Dir => Vector2.ClampMagnitude(_direction,1);
    private int _coins;
    private Dictionary<Vector2, WalkDirection> _directionMap;
    [SerializeField,ReadOnly] private WalkDirection _currentDirection;
    public WalkDirection CurrentDirection => _currentDirection;
    public delegate void DirectionChanged(WalkDirection currentDirection);
    public event DirectionChanged OnDirectionChanged;
    
    //Clothes
    [SerializeField]private Clothes _currentCloth;
    public Clothes CurrentCloth => _currentCloth;
    private List<Clothes> _clothes;
    public PlayerData()
    {
        Initializer();
    }

    void Initializer()
    {
        DirectionMapper();
    }
    private void  DirectionMapper()
    {
        _directionMap = new Dictionary<Vector2, WalkDirection>
        {
            { Vector2.right, WalkDirection.East },
            { Vector2.left, WalkDirection.West },
            { Vector2.up, WalkDirection.North },
            { Vector2.down, WalkDirection.South },
            { Vector2.one, WalkDirection.East },
            { -Vector2.one, WalkDirection.West },
            { new Vector2(-1, 1), WalkDirection.West },
            { new Vector2(1, -1), WalkDirection.East }
        };
    }
    public void UpdateCoins(int updateBy)
    {
        _coins += updateBy;
    }

    public void UpdateDirection(float x, float y)
    {
        var aux = _currentDirection;
        _direction.x = x;
        _direction.y = y;
        _currentDirection = _directionMap.GetValueOrDefault(_direction, WalkDirection.None);
        if (aux != _currentDirection)
        {
            OnDirectionChanged?.Invoke(_currentDirection);
        }
    }

    public void ChangeCloth(Clothes clothe)
    {
        _currentCloth = clothe;
    }
}
}
