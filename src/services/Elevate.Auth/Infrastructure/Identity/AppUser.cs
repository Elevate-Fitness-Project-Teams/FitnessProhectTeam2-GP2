using Microsoft.AspNetCore.Identity;

namespace Elevate.Auth.Infrastructure.Identity
{
    public class AppUser :IdentityUser<Guid>
    {
        public FullName Name { get; set; } = null!;
        public bool RequiresProfileCompletion { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
