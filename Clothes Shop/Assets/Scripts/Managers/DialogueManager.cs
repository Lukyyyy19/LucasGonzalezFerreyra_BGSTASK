using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Managers
{
    public class DialogueManager : SingeltonMonoBehaviour<DialogueManager>
    {
        [SerializeField] private TextMeshProUGUI _dialogueOwner;
        [SerializeField] private TextMeshProUGUI _dialogueLine;
        [SerializeField] private GameObject _panel;
        private bool _currentLineFinished;
        protected override void Awake()
        {
            base.Awake();
            _panel.SetActive(false);
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))&&_currentLineFinished)
            {
                _panel.SetActive(false);
                ScreenManager.Instance.Resume();
            }
        }

        public void WriteDialogue(Dialogue dialogue)
        {
            ScreenManager.Instance.Pause(true);
            Tween writer;
            _panel.SetActive(true);
            _dialogueOwner.text = dialogue.owner;
            string line = "";
            writer = DOTween.To(() => line, x => line = x, dialogue.lines[0], dialogue.lineSpeed).OnUpdate(() =>
            {
                _dialogueLine.text = line;

            });
            writer.OnComplete(() => { _currentLineFinished = true; });
        }
    }
}
