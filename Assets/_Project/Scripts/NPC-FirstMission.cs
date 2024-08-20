using DG.Tweening;
using DialoguSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class NPC_FirstMission : MonoBehaviour, IHaveDialogue
    {
        public int Id { get; }
        public bool isDone { get; private set; }
        public DialogueLine _dialogueLine => dialogueLine;
        [SerializeField] private DialogueLine dialogueLine;

        // [SerializeField] private GameObject BookGameObject;
        // [SerializeField] private Transform bookDestination;


        public void OnDialogueCompleted()
        {
            isDone = true;
            // BookGameObject.SetActive(true);
            // BookGameObject.transform.DOMove(bookDestination.position, 2f);
            // UIDialogue.Instance.StartCo(new DialogueBox("NPC", "Bu kitap sana yol gosterecek. Yediginden emin ol."), 1, 5);
        }
    }
}