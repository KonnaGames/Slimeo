using DG.Tweening;
using DialoguSystem;
using UnityEngine;

namespace Test
{
    public class EatTester : MonoBehaviour, IEatable, IHaveDialogue
    {
        public bool IsTriggered { get; private set; }
        public eSize size { get; } = eSize.Small;

        public void OnAte()
        {
            if (IsTriggered) return;

            IsTriggered = true;
            transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));   
        }

        public int Id { get; }
        public bool isDone { get; }
        [SerializeField] private DialogueLine _dialogueLine = new();
        public DialogueLine GetDialogueLine => _dialogueLine;
    }
}