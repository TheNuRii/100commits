namespace MySpot.Api.Exceptions;

public class EmptyLicensePlateException : Exception
{
    public EmptyLicensePlateException() : base("License plate is empty")
    {
        
    }
}
