using System;
using UnityEngine;

namespace Npc
{
    public class NpcView:MonoBehaviour
    {
        private NpcController _npcController;

        private void Awake()
        {
            _npcController = new NpcController(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _npcController.OnCollideEnter(other.collider);
        }
    }
}