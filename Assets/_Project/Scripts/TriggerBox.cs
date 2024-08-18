using UnityEngine;

namespace DefaultNamespace
{
    public class TriggerBox : MonoBehaviour
    {
        public bool isTriggered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController controller))
            {
                isTriggered = true;
            }
        }
    }
}