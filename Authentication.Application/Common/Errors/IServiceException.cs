using System.Net;

namespace Authentication.Application.Common.Errors;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }

    public string ErrorMessage { get; }
}
