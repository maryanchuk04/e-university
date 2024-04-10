namespace EUniversity.Core.Exceptions;

[Serializable]
public class ServiceClientUnexpectedResponseCodeException : Exception
{
    public int StatusCode { get; set; }
    public int[] ExpectedStatusCodes { get; set; } = [];
    public Uri RootUri { get; set; }
    public string RelativeUri { get; set; }
    public string Method { get; set; }

    public ServiceClientUnexpectedResponseCodeException() : base("Unexpected HTTP status code") { }
    public ServiceClientUnexpectedResponseCodeException(string message) : base(message) { }
    public ServiceClientUnexpectedResponseCodeException(string message, Exception innerException) : base(message, innerException) { }

    public override string ToString()
    {
        return $"Status Code: [{StatusCode}]. ExpectedStatusCodes: [{(ExpectedStatusCodes.Length != 0 ? string.Join(',', ExpectedStatusCodes) : string.Empty)}]. RootUri: [{RootUri}]. RelativeUri: [{RelativeUri}]. Method: [{Method}]. Base:\n{base.ToString()}";
    }
}