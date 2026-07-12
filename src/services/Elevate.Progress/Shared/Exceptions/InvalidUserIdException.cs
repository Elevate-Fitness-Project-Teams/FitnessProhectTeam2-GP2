namespace Elevate.Progress.Shared.Exceptions
{
    public class InvalidUserIdException : BadRequestException
    {
        public InvalidUserIdException()
            : base(
                "VAL_INVALID_USER_ID",
                "The provided user id is invalid.")
        {
        }
    }
}
