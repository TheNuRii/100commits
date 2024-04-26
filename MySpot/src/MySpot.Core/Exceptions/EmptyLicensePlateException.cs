namespace MySpot.Core.Exceptions;

public sealed class EmptyLicensePlateException : CustomException
{
    public EmptyLicensePlateException() : base("License plate is empty")
    {
    }
}