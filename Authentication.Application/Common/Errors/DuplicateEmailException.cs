namespace Authentication.Application.Common.Errors;

public class DuplicateEmailException : Exception
{
   public DuplicateEmailException() : base("Duplicated Email") { }
}
