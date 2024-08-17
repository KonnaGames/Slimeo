using DialoguSystem;
using UnityEngine;

namespace Test
{
    public class EatTester : MonoBehaviour, IEatable, IHaveDialogue
    {
        public eSize size { get; } = eSize.Small;

        public void OnAte()
        {
            Destroy(gameObject);
        }

        public int Id { get; }
        public bool isDone { get; }
        [SerializeField] private DialogueLine _dialogueLine = new();
        public DialogueLine GetDialogueLine => _dialogueLine;
    }
}