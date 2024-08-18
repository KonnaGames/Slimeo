using DialoguSystem;
using UnityEngine;

namespace Test
{
    [SelectionBase]
    public class PlayerEaterTest : MonoBehaviour
    {
        private DialogueLine currentDialogue;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IEatable eatable))
            {
                // Puff Yapicak
                // Havali Seyler olucak
                eatable.OnAte();
                // transform.localScale *= 1.2f;
            }
        }
    }
}