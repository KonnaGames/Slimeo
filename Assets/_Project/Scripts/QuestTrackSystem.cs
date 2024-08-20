using System;
using System.Collections;
using Test;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class QuestTrackSystem : MonoBehaviour
    {
        private int questIndex = 0;
        
        #region QuestReferance

        public NPC_FirstMission FirstMission;
        public NPC_Book npcBook;
        public TriggerBox CaveDoor;
        public TriggerBox HazineOdasi;
        public CaveDoor hazineDoor;

        [Header("Musics")] 
        public AudioClip bossFight;
        public AudioClip slimeRush;
        public AudioClip endGame;
        
        
        
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
            
            UIDialogue.Instance.UpdateQuest("Walk To The Cave");
            yield return new WaitUntil(() => CaveDoor.isTriggered);
            // 2 Ejderha Gorevi
            UIDialogue.Instance.UpdateQuest("Beat All The Slimes");
            MapAmbientMusic.Instance.ChangeMusic(slimeRush);
            
            yield return new WaitUntil(() => EnemyManager.Instance.isAllEnemyDead);
            
            UIDialogue.Instance.UpdateQuest("Kill The Lizard");
            MapAmbientMusic.Instance.ChangeMusic(bossFight);

            yield return new WaitUntil(() => EnemyManager.Instance.isDragonDead);
            hazineDoor.ToggleDoor(true);
            
            UIDialogue.Instance.UpdateQuest("Find The Tresure Room");
            MapAmbientMusic.Instance.ChangeMusic(endGame);

            yield return new WaitUntil(() => HazineOdasi.isTriggered);
            
            UIDialogue.Instance.UpdateQuest("Congrats.. You did it");

            yield return new WaitForSeconds(5);

            Application.Quit();

            yield return null;
        }
    }
}