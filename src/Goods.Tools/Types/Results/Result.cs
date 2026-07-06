using Goods.Tools.Types.Errors;

namespace Goods.Tools.Types.Results;

public record Result(Error[] Errors) : BaseResult(Errors)
{
    public static Result Success() => new(Errors: []);
    public static Result Fail(String message) => new(Errors: [message.ToError()]);
    public static Result Fail(BaseResult result) => new(Errors: result.Errors);
}