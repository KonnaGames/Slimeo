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
        [SerializeField] private GameObject DialogueIndicator;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI questTrackText;

        private void Awake()
        {
            Instance = this;
            
            HideDialogue();
            ToggleIndicator(false);
            questTrackText.text = "";
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
            ToggleIndicator(false);
        }

        public void ToggleIndicator(bool toggle)
        {
            DialogueIndicator.SetActive(toggle);
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
        }

        public void UpdateQuest(string text)
        {
            questTrackText.text = text;
        }
    }
}