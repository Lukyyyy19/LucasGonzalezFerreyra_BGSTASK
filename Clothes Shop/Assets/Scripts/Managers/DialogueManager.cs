using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace Managers
{
    public class DialogueManager : SingeltonMonoBehaviour<DialogueManager>
    {
        [SerializeField] private TextMeshProUGUI _dialogueOwner;
        [SerializeField] private TextMeshProUGUI _dialogueLine;
        [SerializeField] private GameObject _panel;
        public void WriteDialogue(Dialogue dialogue)
        {
            Tween writer;
            _panel.SetActive(true);
            _dialogueOwner.text = dialogue.owner;
            string line = "";
            writer = DOTween.To(() => line, x => line = x, dialogue.lines[0], dialogue.lineSpeed).OnUpdate(() =>
            {
                _dialogueLine.text = line;

            });
        }
    }
}
