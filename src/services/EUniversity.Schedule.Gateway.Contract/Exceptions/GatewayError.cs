namespace EUniversity.Schedule.Gateway.Contract.Exceptions;

public class GatewayError
{
    public string ReasonCode { get; set; }
    public string Message { get; set; }
    public string RequestCorrelationId { get; set; }
}
