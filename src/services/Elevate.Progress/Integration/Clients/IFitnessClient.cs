namespace Elevate.Progress.Integration.Clients
{
    public interface IFitnessClient
    {
        Task<FitnessStatsResponse?> GetFitnessStatsAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
