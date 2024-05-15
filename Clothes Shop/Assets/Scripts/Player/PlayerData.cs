using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Enums;
using Unity.Collections;

namespace Player
{
    [Serializable]
    public class PlayerData
    {
        //Movement
        private float _maxSpeed = 5;
        private float _currentSpeed;
        public float Speed => _currentSpeed;
        private Vector2 _direction;
        public Vector2 Dir => Vector2.ClampMagnitude(_direction, 1);
        [SerializeField]private int _coins = 20;
        public int Coins => _coins;
        private Dictionary<Vector2, WalkDirection> _directionMap;
        [SerializeField, ReadOnly] private WalkDirection _currentDirection;
        public WalkDirection CurrentDirection => _currentDirection;

        public delegate void DirectionChanged(WalkDirection currentDirection);

        public event DirectionChanged OnDirectionChanged;

        //Clothes
        [SerializeField] private Clothes _currentCloth;
        [SerializeField] private Clothes _currentHat;
        public bool HasHat => _currentClothes.Any(x=>x.id>10);
        public Clothes CurrentCloth => _currentCloth;
        public Clothes CurrentHat => _currentHat;
        [SerializeField]private List<Clothes> _clothes = new List<Clothes>();
        [SerializeField] private List<Clothes> _currentClothes = new List<Clothes>();
        public Action<Clothes> OnClotheChanged;
        public PlayerData()
        {
            Initializer();
        }

        void Initializer()
        {
            _currentSpeed = _maxSpeed;
            DirectionMapper();
        }

        private void DirectionMapper()
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
            _coins = Mathf.Clamp(_coins + updateBy,0,99);
            
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

        public void Pause()
        {
            _currentDirection = WalkDirection.Pause;
            _currentSpeed = 0;
        }

        public void Resume()
        {
            _currentDirection = WalkDirection.None;
            _currentSpeed = _maxSpeed;
        }

        public void UpdateSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed;
        }

        public void ChangeCloth(Clothes clothe)
        {
            _currentCloth = clothe;
            if (!_clothes.Contains(clothe)) _clothes.Add(clothe);
            if (!_currentClothes.Contains(clothe)) _currentClothes.Add(clothe);
            OnDirectionChanged?.Invoke(WalkDirection.None);
        }

        public void ChangeHat(Clothes clothe)
        {
            _currentHat = clothe;
            if (!_clothes.Contains(clothe)) _clothes.Add(clothe);
            if (!_currentClothes.Contains(clothe)) _currentClothes.Add(clothe);
            OnDirectionChanged?.Invoke(WalkDirection.None);
        }

        public void AddClothe(Clothes clothe)
        {
            _clothes.Add(clothe);
        }

        public void Remove(Clothes clothes)
        {
            _clothes.Remove(clothes);
        }

        public List<Clothes> GetClothes()
        {
            return _clothes;
        }

        public List<Clothes> GetCurrentClothes()
        {
            return _currentClothes;
        }
    }
}