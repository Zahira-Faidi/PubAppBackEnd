using System.Net;

namespace Authentication.Application.Common.Errors;

public class DuplicateEmailException : Exception, IServiceException
{


    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email Already Exists";
}