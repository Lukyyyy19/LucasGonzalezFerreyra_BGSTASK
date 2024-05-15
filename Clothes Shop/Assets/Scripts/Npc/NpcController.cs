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

        public void StartDialogue()
        {
            DialogueManager.Instance.WriteDialogue(_view.GetDialogue());
        }

        public void Collide(Collider2D col)
        {
            if (col.CompareTag("Chest"))
            {
                GameManager.Instance.ChestDelivered(5);
            }
        }
    }
}
