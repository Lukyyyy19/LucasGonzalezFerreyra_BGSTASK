using System;
using System.Collections;
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
        [SerializeField]private bool _currentLineFinished;
        private Dialogue _currentDialogue;

        protected override void Awake()
        {
            base.Awake();
            _panel.SetActive(false);
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && _currentLineFinished)
            {
                _panel.SetActive(false);
                ScreenManager.Instance.Resume();
                _currentDialogue.Consecuence?.Invoke();
                _currentLineFinished = false;
            }
        }

        public void WriteDialogue(Dialogue dialogue)
        {
            StartCoroutine(WriteDialogueCorutine(dialogue));
        }

        private IEnumerator WriteDialogueCorutine(Dialogue dialogue)
        {
            _currentDialogue = dialogue;
            ScreenManager.Instance.Pause(true);
            Tween writer;
            _panel.SetActive(true);
            _dialogueOwner.text = dialogue.owner;
            foreach (var lines in dialogue.lines)
            {
                _currentLineFinished = false;
                string line = "";
                writer = DOTween.To(() => line, x => line = x, lines, dialogue.lineSpeed).OnUpdate(() =>
                {
                    _dialogueLine.text = line;
                });
                yield return new WaitForSeconds(1.5f);
            }
            _currentLineFinished = true;
            Debug.Log("assa");
        }
    }
}