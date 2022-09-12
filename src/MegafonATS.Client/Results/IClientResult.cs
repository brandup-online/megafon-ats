namespace MegafonATS.Client.Results
{
    public interface IClientResult
    {
        bool IsSuccess { get; }
        ErrorResponse Error { get; }
        object Data { get; }
    }
}