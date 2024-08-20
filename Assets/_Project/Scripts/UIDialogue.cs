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

        public bool AutoDiagloue = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            
            HideDialogue();
            ToggleIndicator(false);
            questTrackText.text = "";
            
            
            DontDestroyOnLoad(this.gameObject);
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
            if (AutoDiagloue) return;
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

        public void StartAutoDialogue(IHaveDialogue haveDialogue, float delay)
        {
            StartCoroutine(StartAutoDialogueCO(haveDialogue , delay));
        }
        
        private IEnumerator StartAutoDialogueCO(IHaveDialogue haveDialogue, float delay)
        {
            AutoDiagloue = true;

            int currentIndex = 0;
            while (currentIndex < haveDialogue._dialogueLine._dialoguLines.Count)
            {
                ShowDialogue(haveDialogue.GetNextDialogue());
                currentIndex++;
                yield return new WaitForSeconds(delay);
            }
            
            HideDialogue();
            AutoDiagloue = false;

            yield return null;
        }
        
        public void UpdateQuest(string text)
        {
            questTrackText.text = text;
        }
    }
}