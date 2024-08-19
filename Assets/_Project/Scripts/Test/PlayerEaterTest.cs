using UnityEngine;

namespace Test
{
    [SelectionBase]
    public class PlayerEaterTest : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IEatable eatable))
            {
                // Puff Yapicak
                // Havali Seyler olucak
                if (eatable.IsTriggered) return;
                
                eatable.OnAte();
            }
        }
    }
}