using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class HatsView : MonoBehaviour
{
    [SerializeField] private List<AnimationClip> animationClips;
    [SerializeField] private Animator _animator;
    
    public void ChangeAnimation(int clothId,WalkDirection currentDirection)
    {
        var x = animationClips.Find(x => x.name == ("Walk" + currentDirection + '_' + clothId));
        _animator.CrossFade(x==null?"Idle_"+clothId:x.name,0);
    }
}
