namespace MySpot.Api.Exceptions;

public class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
        
    }
}

