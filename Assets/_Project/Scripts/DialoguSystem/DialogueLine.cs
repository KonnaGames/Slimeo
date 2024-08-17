using System;
using System.Collections.Generic;

namespace DialoguSystem
{
    [Serializable]
    public class DialogueLine
    {
        public int currentIndex = 0;
        public bool isDone = false;
        public List<DialogueBox> _dialoguLines = new();

        public DialogueBox PlayDialgoue()
        {
            if (currentIndex >= _dialoguLines.Count) return null;

            return _dialoguLines[currentIndex++];
        }
    }
}