namespace MySpot.Api.Exceptions;

public class InvaildEmployeeNameException : CustomException
{
    public string EmployeeName { get; }

    public InvaildEmployeeNameException(string employeeName) 
        : base($"Employee name: {employeeName} is invalid")
    {
        EmployeeName = employeeName;
    }
}