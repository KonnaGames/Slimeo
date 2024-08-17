namespace DialoguSystem
{
    public interface IHaveDialogue
    {
        int Id { get; }
        bool isDone { get; }
        DialogueLine GetDialogueLine { get; }
    }
}