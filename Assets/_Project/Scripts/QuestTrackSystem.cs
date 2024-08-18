using System;
using System.Collections;
using Test;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class QuestTrackSystem : MonoBehaviour
    {
        private int questIndex = 0;
        
        #region QuestReferance

        public NPC_FirstMission FirstMission;
        public NPC_Book npcBook;
        public TriggerBox doorTriggerBox;
        public Dragon dragon;
        public TriggerBox dragonTriggerBox;
        
        
        
        #endregion

        private void Start()
        {
            StartCoroutine(AllQuestsCo());
        }

        private IEnumerator AllQuestsCo()
        {
            // 1 Kapi Acma Gorevi
            UIDialogue.Instance.UpdateQuest("Talk To NPC");
            yield return new WaitUntil(() => FirstMission.isDone);
            
            UIDialogue.Instance.UpdateQuest("Kitabi Ye");
            yield return new WaitUntil(() => npcBook.IsTriggered);
            
            UIDialogue.Instance.UpdateQuest("Walk To The Door");
            yield return new WaitUntil(() => doorTriggerBox.isTriggered);
            
            // 2 Ejderha Gorevi
            UIDialogue.Instance.UpdateQuest("Ejderhayi Bul");
            yield return new WaitUntil(() => dragonTriggerBox.isTriggered);
            
            UIDialogue.Instance.UpdateQuest("Ejderhayla Konus");
            yield return new WaitUntil(() => dragon.isDone);
            UIDialogue.Instance.UpdateQuest("");










            yield return null;
        }
    }
}