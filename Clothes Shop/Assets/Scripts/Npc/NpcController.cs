using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Npc
{
    public class NpcController
    {
        private NpcView _view;
        public NpcController(NpcView view)
        {
            _view = view;
        }

        public void OnCollideEnter(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                DialogueManager.Instance.WriteDialogue(_view.GetDialogue());
            }
        }

        public void OnCollideExit(Collider2D col)
        {
            
        }
    }
}
