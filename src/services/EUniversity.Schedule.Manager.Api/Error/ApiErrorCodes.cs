namespace EUniversity.Schedule.Manager.Api.Error;

public static class ApiErrorCodes
{
    public static (string code, string message) InternalServerError => ("internal_server_error", "Something happened on the server.");
    public static (string code, string message) InvalidRequest => ("invalid_request", "Request payload should be specified.");
}

