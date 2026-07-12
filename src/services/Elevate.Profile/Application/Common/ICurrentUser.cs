namespace Elevate.Profile.Application.Common
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        string? UserName { get; }
    }
}
