namespace ErrorHanding;

public static class ErrorCatalog
{
    private static readonly Dictionary<ErrorCode, ErrorDetail> _errors = new()
    {
        { ErrorCode.LoginFail, new ErrorDetail(1059, "Đăng nhập bị lỗi", 404) },
        // Add more mappings here
    };

    public static ErrorDetail Get(ErrorCode code) => _errors[code];
}