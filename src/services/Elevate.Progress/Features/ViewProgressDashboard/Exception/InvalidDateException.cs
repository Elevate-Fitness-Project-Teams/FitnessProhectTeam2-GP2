using Elevate.Progress.Shared.Exceptions;

namespace Elevate.Progress.Features.ViewProgressDashboard.Exception
{
    public class InvalidDateException : BadRequestException
    {
        public InvalidDateException()
            : base(
                "VAL_INVALID_DATE",
                "The start date cannot be after the end date.")
        {
        }
    }
}
