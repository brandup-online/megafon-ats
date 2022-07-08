namespace MegafonATS.Client
{
    public interface IClientResult
    {
        bool IsSuccess { get; }
        string Error { get; }
        object Result { get; }
    }
}