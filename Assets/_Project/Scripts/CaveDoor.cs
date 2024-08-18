using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class CaveDoor : MonoBehaviour
    {
        [SerializeField] private Transform Fence;
        
        public void ToggleDoor(bool toggle)
        {
            if (toggle)
            {
                // Kapi ac
                Fence.transform.DOMove(transform.localPosition + new Vector3(0,5.5f,0), 5f);
            }
        }
    }
}