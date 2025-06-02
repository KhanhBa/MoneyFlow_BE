namespace ErrorHanding;

public class ErrorDetail
{
    public int Code { get; }
    public string Message { get; }
    public int HttpStatus { get; }

    public ErrorDetail(int code, string message, int httpStatus)
    {
        Code = code;
        Message = message;
        HttpStatus = httpStatus;
    }
}