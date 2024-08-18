using DG.Tweening;
using DialoguSystem;
using UnityEngine;

namespace Test
{
    public class EatTester : MonoBehaviour, IEatable
    {
        public bool IsTriggered { get; private set; }
        public eSize size { get; } = eSize.Small;

        public void OnAte()
        {
            if (IsTriggered) return;

            IsTriggered = true;
            transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));   
        }
    }
}