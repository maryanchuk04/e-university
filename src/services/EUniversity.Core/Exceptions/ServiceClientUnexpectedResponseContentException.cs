namespace EUniversity.Core.Exceptions;

[Serializable]
internal class ServiceClientUnexpectedResponseContentException : Exception
{
    public ServiceClientUnexpectedResponseContentException()
    {
    }

    public ServiceClientUnexpectedResponseContentException(string? message) : base(message)
    {
    }

    public ServiceClientUnexpectedResponseContentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public string Method { get; set; }
    public Uri RootUri { get; set; }
    public string RelativeUri { get; set; }
    public int StatusCode { get; set; }
    public string Content { get; set; }
}