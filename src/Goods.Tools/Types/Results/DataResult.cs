using Goods.Tools.Types.Errors;

namespace Goods.Tools.Types.Results;

public record DataResult<T>(T Data, Error[] Errors) : BaseResult(Errors)
{
    public Boolean IsFail(out T data)
    {
        data = Data;
        return !IsSuccess;
    }

    public static DataResult<T> Success(T data) => new(Data: data, Errors: []);
    public static DataResult<T> Fail(String message) => new(Data: default!, Errors: [message.ToError()]);
    public static DataResult<T> Fail(BaseResult result) => new(Data: default!, Errors: result.Errors);

    public Result ToResult() => IsSuccess ? Result.Success() : Result.Fail(this);
}