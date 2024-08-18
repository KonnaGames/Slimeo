using System;
using System.Collections;
using DialoguSystem;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIDialogue : MonoBehaviour
    {
        public static UIDialogue Instance;

        [SerializeField] private GameObject Window;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Awake()
        {
            Instance = this;
            
            HideDialogue();
        }

        public void ShowDialogue(DialogueBox dialogueBox)
        {
            Window.gameObject.SetActive(true);
            speakerText.text = dialogueBox.SpeakerName;
            dialogueText.text = dialogueBox.Text;
        }

        public void HideDialogue()
        {
            Window.gameObject.SetActive(false);
        }

        public void StartCo(DialogueBox dialogueBox, float delay, float duration)
        {
            StartCoroutine(CoShortDialogue(dialogueBox, delay, duration));
        }

        private IEnumerator CoShortDialogue(DialogueBox dialogueBox, float delay, float duration)
        {
            yield return new WaitForSeconds(delay);
            ShowDialogue(dialogueBox);
            yield return new WaitForSeconds(duration);
            HideDialogue();
        }
    }
}