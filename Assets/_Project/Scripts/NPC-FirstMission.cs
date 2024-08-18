using DialoguSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class NPC_FirstMission : MonoBehaviour, IHaveDialogue
    {
        public int Id { get; }
        public bool isDone { get; private set; }
        public DialogueLine _dialogueLine => dialogueLine;
        [SerializeField] private DialogueBox DyingDialogue;
        [SerializeField] private DialogueLine dialogueLine;


        public void OnDialogueCompleted()
        {
            Destroy(gameObject);
            isDone = true;
            UIDialogue.Instance.StartCo(DyingDialogue, 1f, 2f);
        }
    }
}