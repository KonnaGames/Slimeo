using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialoguSystem
{
    [Serializable]
    public class DialogueBox
    {
        public string SpeakerName;
        [TextArea(3,3)]
        public string Text;
        public UnityEvent Event;

        public DialogueBox(string SpeakerName, string Text)
        {
            this.SpeakerName = SpeakerName;
            this.Text = Text;
        }


        public DialogueBox()
        {
            
        }
    }
}