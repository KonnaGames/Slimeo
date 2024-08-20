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
            yield return new WaitForSeconds(3);
            UIDialogue.Instance.StartAutoDialogue(this, 1);
        }

        public void OnDialogueCompleted()
        {
            Debug.Log("AutoDialog Completed");
        }
    }
}