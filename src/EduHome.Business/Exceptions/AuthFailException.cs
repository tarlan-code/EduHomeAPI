namespace EduHome.Business.Exceptions;
public sealed class AuthFailException : Exception
{
    public AuthFailException(string message) : base(message)
    {
    }
}

