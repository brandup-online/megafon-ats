namespace MegafonATS.Client.Results
{
    public class ClientResult<T> : IClientResult
    {
        private ClientResult() { }

        public bool IsSuccess { get; private set; }
        public ErrorResponse Error { get; private set; }

        public T Data { get; set; }
        object IClientResult.Data => Data;

        public static ClientResult<T> Success() => new() { IsSuccess = true };
        public static ClientResult<T> Success(T result) => new() { IsSuccess = true, Data = result };
        public static ClientResult<T> SetError(ErrorResponse errorResponse) => new() { IsSuccess = false, Error = errorResponse };
    }

    public class ClientResult : IClientResult
    {
        private ClientResult() { }

        public bool IsSuccess { get; private set; }
        public ErrorResponse Error { get; private set; }

        public bool Data { get; set; }
        object IClientResult.Data => Data;

        public static ClientResult Success() => new() { IsSuccess = true, Data = true };
        public static ClientResult SetError(ErrorResponse errorResponse) => new() { IsSuccess = false, Error = errorResponse, Data = false };
    }
}