using Elevate.Profile.Application.Common;

namespace Elevate.Profile.Infrastructure.Authentication
{
    public class CurrentUser : ICurrentUser, ICurrentUserInitializer
    {
        public Guid? UserId { get; set; }

        public string? UserName { get; set; }
        //public string? Username { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
