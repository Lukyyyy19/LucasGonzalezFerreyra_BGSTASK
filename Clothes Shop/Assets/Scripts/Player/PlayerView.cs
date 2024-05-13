using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ClothesView _clothesView;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<AnimationClip> _animationClips;
    private Dictionary<WalkDirection, int> _animationsMap;
    [SerializeField] public Clothes _startCloth;
    private void AnimationsMapper()
    {
        _animationsMap = new Dictionary<WalkDirection, int>()
        {
            { WalkDirection.None,Animator.StringToHash("Idle")},
            { WalkDirection.North, Animator.StringToHash("WalkNorth") },
            { WalkDirection.South,Animator.StringToHash("WalkSouth") },
            { WalkDirection.East, Animator.StringToHash("WalkEast")},
            // { WalkDirection.NorthEast, Animator.StringToHash("WalkEast") },
            // { WalkDirection.SouthEast, Animator.StringToHash("WalkEast") },
            { WalkDirection.West, Animator.StringToHash("WalkWest") },
            // { WalkDirection.NorthWest,Animator.StringToHash("WalkWest") },
            // { WalkDirection.SouthWest, Animator.StringToHash("WalkWest") },
        };
    }

    private void Start()
    {
        _playerController = new PlayerController(this);
        AnimationsMapper();
    }

    private void Update()
    {
        _playerController.Move();
    }

   public void ChangeAnimation(WalkDirection currentDirection)
    {
        if (_animationsMap.TryGetValue(currentDirection, out int animationClipHash))
        {
            _animator.CrossFade(animationClipHash,0);
        }
        _clothesView.ChangeAnimation(_playerController.GetCurrentCloth().id,currentDirection);
    }

    private void OnDisable()
    {
        _playerController.OnDestroy();
    }
}