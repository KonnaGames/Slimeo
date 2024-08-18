using DialoguSystem;
using UnityEngine;

namespace Test
{
    [SelectionBase]
    public class PlayerEaterTest : MonoBehaviour
    {
        private DialogueLine currentDialogue;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) Debug.Log(currentDialogue.PlayDialgoue()?.Text);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHaveDialogue dialogue)) currentDialogue = dialogue.GetDialogueLine;
        }


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