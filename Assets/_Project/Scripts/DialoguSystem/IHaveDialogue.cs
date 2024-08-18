namespace DialoguSystem
{
    public interface IHaveDialogue
    {
        int Id { get; }
        bool isDone { get; }
        DialogueLine _dialogueLine { get; }
        DialogueBox GetNextDialogue()
        {
            if (isDone) return new DialogueBox();
            
            if (_dialogueLine.currentIndex >= _dialogueLine._dialoguLines.Count)
            {
                OnDialogueCompleted();
                return new DialogueBox();
            }
            var dialogueBox = _dialogueLine.GetDialogue(_dialogueLine.currentIndex++);

            return dialogueBox;
        }
        void OnDialogueCompleted();
    }
}