using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinEarnedBehaviour : MonoBehaviour
{
    private void Awake()
    {
        transform.DOMoveY(1f, 2f).OnComplete(()=>Destroy(gameObject));
    }
}
