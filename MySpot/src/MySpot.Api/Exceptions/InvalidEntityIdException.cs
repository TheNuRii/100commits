namespace MySpot.Api.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public Guid EntityId { get; }

    public InvalidEntityIdException(Guid entityId) 
        : base($"Entity id: {entityId} is invalid")
    {
        EntityId = entityId;
    }
}