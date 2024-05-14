using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPauseable
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private ClothesView _clothesView;
        [SerializeField] private HatsView _hatsView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb;
        [SerializeField] private List<AnimationClip> _animationClips;
        private Dictionary<WalkDirection, int> _animationsMap;
        [SerializeField] public Clothes _startCloth;

        private void AnimationsMapper()
        {
            _animationsMap = new Dictionary<WalkDirection, int>()
            {
                { WalkDirection.None, Animator.StringToHash("Idle") },
                { WalkDirection.North, Animator.StringToHash("WalkNorth") },
                { WalkDirection.South, Animator.StringToHash("WalkSouth") },
                { WalkDirection.East, Animator.StringToHash("WalkEast") },
                { WalkDirection.West, Animator.StringToHash("WalkWest") },
            };
        }

        private void Awake()
        {
            _playerController = new PlayerController(this);
            AnimationsMapper();
        }

        private void Start()
        {
            _playerController.Start();
            ScreenManager.Instance.AddPauseable(this);
        }

        private void FixedUpdate()
        {
            _playerController.Move();
        }

        public void ChangeAnimation(WalkDirection currentDirection)
        {
            _animator.speed = currentDirection == WalkDirection.Pause ? 0 : 1;
            if (_animationsMap.TryGetValue(currentDirection, out int animationClipHash))
            {
                _animator.CrossFade(animationClipHash, 0);
            }

            if (_playerController.GetCurrentCloth())
            {
                _clothesView.gameObject.SetActive(true);
                _clothesView.ChangeAnimation(_playerController.GetCurrentCloth().id, currentDirection);
            }
            else
            {
                _clothesView.gameObject.SetActive(false);
            }
            if (_playerController.GetCurrentHat())
            {
                _hatsView.gameObject.SetActive(true);
                _hatsView.ChangeAnimation(_playerController.GetCurrentHat().id, currentDirection);
            }
            else
            {
                _hatsView.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        private void OnDisable()
        {
            _playerController.OnDestroy();
            ScreenManager.Instance.RemovePauseable(this);
        }

        public void Pause(bool isDialoguePause = false)
        {
            _playerController.Pause();
        }

        public void Resume()
        {
            _playerController.Resume();
        }
    }
}