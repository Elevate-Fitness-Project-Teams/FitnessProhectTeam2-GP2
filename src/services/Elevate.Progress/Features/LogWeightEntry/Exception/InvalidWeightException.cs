using Elevate.Progress.Shared.Exceptions;

namespace Elevate.Progress.Features.LogWeightEntry.Exception
{
    public class InvalidWeightException : BadRequestException
    {
        public InvalidWeightException()
            : base(
                "VAL_INVALID_WEIGHT",
                "Weight must be between 40 kg and 200 kg.")
        {
        }
    }
}
