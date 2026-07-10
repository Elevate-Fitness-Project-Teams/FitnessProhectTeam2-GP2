namespace Elevate.Profile.Application.Common
{
    public interface ICurrentUserInitializer
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
