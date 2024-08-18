using System;
using DefaultNamespace;
using DialoguSystem;
using UnityEngine;

namespace Player
{
    public class DialogueReader : MonoBehaviour
    {
        private IHaveDialogue currentDialogue;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentDialogue == null) return;
                
                if (!currentDialogue.isDone)
                {
                    UIDialogue.Instance.ShowDialogue(currentDialogue.GetNextDialogue());
                }
                else
                {
                    UIDialogue.Instance.HideDialogue();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHaveDialogue dialogue))
            {
                if (dialogue.isDone) return;
                currentDialogue = dialogue;
                UIDialogue.Instance.ToggleIndicator(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IHaveDialogue dialogue))
            {
                currentDialogue = null;
                UIDialogue.Instance.HideDialogue();
                UIDialogue.Instance.ToggleIndicator(false);
            }
        }
    }
}