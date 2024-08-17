public interface IEatable
{
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