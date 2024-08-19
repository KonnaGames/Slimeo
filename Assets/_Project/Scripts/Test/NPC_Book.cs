using DefaultNamespace;
using DG.Tweening;
using DialoguSystem;
using UnityEngine;

namespace Test
{
    public class NPC_Book : MonoBehaviour, IEatable
    {
        public bool IsTriggered { get; set; }
        public eSize Size { get; } = eSize.Small;

        [SerializeField] private CaveDoor _caveDoor;

        public void OnAte()
        {
            if (IsTriggered) return;

            IsTriggered = true;
            transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InBack).OnComplete(() => KapiyiAc());   
        }

        private void KapiyiAc()
        {
            _caveDoor.ToggleDoor(true);
            Destroy(this.gameObject);
        }
    }
}