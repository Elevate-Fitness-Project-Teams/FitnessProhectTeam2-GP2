using Elevate.Progress.Domain.Enums;

namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class ViewProgressDashboardRequestDto
    {
        public Guid UserId { get; set; }

        public ProgressPeriod? Period { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
