using luxclusif.aggregator.domain.Conts;

namespace luxclusif.aggregator.domain.Exceptions;
public class EntityGenericException : Exception
{
    private readonly string _genericCode = ErrorsCodes.ErrorCode;
    public string Code { get; private set; }

    public EntityGenericException(string? message) : base(message)
    {
        Code = _genericCode;
    }

    public EntityGenericException(string? message, string? code) : base(message)
    {
        Code = code ?? _genericCode;
    }
}