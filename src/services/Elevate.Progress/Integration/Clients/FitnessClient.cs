namespace Elevate.Progress.Integration.Clients
{
    public sealed class FitnessClient : IFitnessClient
    {
        private readonly HttpClient _httpClient;

        public FitnessClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FitnessStatsResponse?> GetFitnessStatsAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<FitnessStatsResponse>(
                $"api/v1/fitness/stats/{userId}",
                cancellationToken);
        }
    }
}
