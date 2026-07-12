namespace Elevate.Profile.Application.Common
{
    public interface ICurrentUserInitializer
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
