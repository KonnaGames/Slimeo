public interface IEatable
{
    bool IsTriggered { get; }
    eSize Size { get; }
    void OnAte();
}

public enum eSize
{
    Small,
    SmallBig,
    Medium,
    MediumBig,
    Big,
    Giant
}