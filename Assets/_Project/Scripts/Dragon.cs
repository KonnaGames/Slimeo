using DialoguSystem;
using UnityEngine;

namespace Test
{
    public class Dragon : MonoBehaviour, IHaveDialogue
    {
        public int Id { get; }
        public bool isDone { get; private set; }
        [SerializeField] private DialogueLine dialogueLine;
        
        public DialogueLine _dialogueLine => dialogueLine;
        public void OnDialogueCompleted()
        {
            isDone = true;
            Debug.Log("Dragon Quest Completed");
        }
    }
}