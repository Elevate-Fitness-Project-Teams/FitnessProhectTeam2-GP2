using Elevate.Progress.Shared.Exceptions;

namespace Elevate.Progress.Features.LogWeightEntry.Exception
{
    public class InvalidDateException : BadRequestException
    {
        public InvalidDateException()
            : base(
                "VAL_INVALID_DATE",
                "The provided date is invalid.")
        {
        }
    }
}
