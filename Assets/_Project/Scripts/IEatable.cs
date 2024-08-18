public interface IEatable
{
    bool IsTriggered { get; }
    eSize size { get; }
    void OnAte();
}

public enum eSize
{
    Small,
    Medium,
    Big,
    Giant
}