namespace Elevate.Progress.Shared.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
            : base(
                "RES_USER_NOT_FOUND",
                "User not found.")
        {
        }
    }
}
