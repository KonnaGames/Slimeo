using System;
using System.Collections;
using DefaultNamespace;
using DialoguSystem;
using UnityEngine;

namespace Player
{
    public class SlimeStartingDialogue : MonoBehaviour, IHaveDialogue
    {
        public int Id { get; }
        public bool isDone { get; }
        [SerializeField] private DialogueLine dialogueLine;
        public DialogueLine _dialogueLine => dialogueLine;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            UIDialogue.Instance.StartAutoDialogue(this, 2);
        }

        public void OnDialogueCompleted()
        {
            Debug.Log("AutoDialog Completed");
        }
    }
}