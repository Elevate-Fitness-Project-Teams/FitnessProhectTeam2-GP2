namespace Elevate.Profile.Application.Common
{
    public interface ICurrentUser
    {
        int? UserId { get; }
        string? UserName { get; }
    }
}
