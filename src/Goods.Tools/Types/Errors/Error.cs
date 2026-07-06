namespace Goods.Tools.Types.Errors;

public struct Error
{
    public String? Key { get; }
    public String Message { get; }

    public Error(String message)
    {
        Key = null;
        Message = message;
    }

    public Error(String? key, String message)
    {
        Key = key;
        Message = message;
    }

    public static implicit operator Error(String str) => new(str);
}

public static class ErrorExtensions
{
    public static Error ToError(this String message) => new(key: null, message);
    public static Error ToError(this String message, String key) => new(key, message);
    public static Error[] ToErrors(this String[] messages) => messages.Select(s => s.ToError()).ToArray();
}