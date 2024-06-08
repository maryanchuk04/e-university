using System.Net;

namespace EUniversity.Core.Error;

/// <summary>
/// Error Response model
/// </summary>
public class ErrorModel
{
    public ErrorModel() { }

    public ErrorModel(int status, (string code, string message) errorCode)
    {
        Status = status;
        Code = errorCode.code;
        Message = errorCode.message;
    }

    public ErrorModel(HttpStatusCode status, (string code, string message) errorCode)
    {
        Status = (int)status;
        Code = errorCode.code;
        Message = errorCode.message;
    }

    public int Status { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
}
