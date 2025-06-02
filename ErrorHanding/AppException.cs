namespace ErrorHanding;

public class AppException : Exception
{
    public ErrorDetail Error { get; }

    public AppException(ErrorCode code) : base(ErrorCatalog.Get(code).Message)
    {
        Error = ErrorCatalog.Get(code);
    }
}