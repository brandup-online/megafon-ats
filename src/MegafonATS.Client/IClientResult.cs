using System.Collections.Generic;

namespace MefafonATS.Model
{
    public interface IClientResult
    {
        bool IsSuccess {get;}
        string Error { get; }
        object Result {get;}
    }
}