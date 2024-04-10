using System.Net;

namespace EUniversity.Gateway.Contract.Exceptions;

public class GatewayException : Exception
{
    /// <summary>
    /// Gateway Exception
    /// </summary>
    public GatewayException(
        string message,
        string reasonCode,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        Exception? innerException = null) : base(message, innerException)
    {
        ReasonCode = reasonCode;
        StatusCode = statusCode;
    }

    public string ReasonCode { get; }
    public HttpStatusCode StatusCode { get; }
}
