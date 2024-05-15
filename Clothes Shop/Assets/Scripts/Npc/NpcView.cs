using System;
using Managers;
using UnityEngine;

namespace Npc
{
    public class NpcView : MonoBehaviour,IDialogue
    {
        private NpcController _npcController;
        [SerializeField] private Dialogue _dialogue;

        private void Awake()
        {
            _npcController = new NpcController(this);
            _dialogue.Consecuence.AddListener(ShopManager.Instance.OpenShop);
        }

        public Dialogue GetDialogue()
        {
            return _dialogue;
        }

        public void StartDialogue()
        {
            _npcController.StartDialogue();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _npcController.Collide(other.collider);
        }
    }
}