using System;

namespace DialoguSystem
{
    [Serializable]
    public class DialogueBox
    {
        public string SpeakerName;
        public string Text;

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