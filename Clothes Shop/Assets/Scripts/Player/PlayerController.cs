using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [Serializable]
    public class PlayerController
    {
        private PlayerView _view;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private bool _isPaused;
        public bool IsPaused => _isPaused;
        private float _castRadius;
        [SerializeField]private LayerMask _interacteableLayer;

        public PlayerController(PlayerView view)
        {
            _view = view;
            _playerData = new PlayerData();
            _playerData.ChangeCloth(_view._startCloth);
            _playerData.OnDirectionChanged += _view.ChangeAnimation;
            _castRadius = 5f;
            _interacteableLayer = LayerMask.GetMask("Interacteable");
        }

        public void Start()
        {
            ShopManager.Instance.InitializePlayerData(_playerData);
            GameManager.Instance.InitializePlayerData(_playerData,_view.transform);
        }

        public void Move()
        {
            if (_isPaused)
            {
                _view.Rb.velocity = Vector2.zero;
                _playerData.UpdateDirection(0, 0);
                return;
            }

            _playerData.UpdateDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

        public Clothes GetCurrentHat()
        {
            return _playerData.CurrentHat;
        }

        public int GetCoins()
        {
            return _playerData.Coins;
        }
        public void CheckForInteractions()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var col = Physics2D.OverlapCircle(_view.transform.position, _castRadius,_interacteableLayer);
                if (col.TryGetComponent(out IDialogue dialogue))
                {
                    dialogue.StartDialogue();
                }
            }
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