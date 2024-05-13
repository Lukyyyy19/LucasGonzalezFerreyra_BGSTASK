using System.Collections;
using System.Collections.Generic;
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
            
        }

        public void OnCollideExit(Collider2D col)
        {
            
        }
    }
}
