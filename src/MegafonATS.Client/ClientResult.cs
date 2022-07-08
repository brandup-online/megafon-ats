namespace MefafonATS.Model
{
    public class ClientResult<T> : IClientResult
    {
        private ClientResult() { }

        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public T Result { get; set; }
        object IClientResult.Result => Result;

        public static ClientResult<T> Success() => new() { IsSuccess = true };
        public static ClientResult<T> Success(T result) => new() { IsSuccess = true, Result = result };
        public static ClientResult<T> SetError(string errorMessage) => new() { IsSuccess = false, Error = errorMessage };
    }
    public class ClientResult : IClientResult
    {
        private ClientResult() { }

        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public bool Result { get; set; }
        object IClientResult.Result => Result;

        public static ClientResult Success() => new() { IsSuccess = true, Result = true};
        public static ClientResult SetError(string errorMessage) => new() { IsSuccess = false, Error = errorMessage, Result = false };
    }
}