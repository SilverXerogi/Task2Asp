using Goods.Tools.Types.Errors;

namespace Goods.Tools.Types.Results;

public abstract record BaseResult
{
    public Error[] Errors { get; private set; }
    public String ErrorsAsString => String.Join("; ", Errors.Select(e => e.Message));

    public Boolean IsSuccess => !Errors.Any();

    protected BaseResult(IEnumerable<Error> errors)
    {
        Errors = errors.ToArray();
    }

    public override String ToString()
    {
        return IsSuccess ? "Успешно" : ErrorsAsString;
    }
}