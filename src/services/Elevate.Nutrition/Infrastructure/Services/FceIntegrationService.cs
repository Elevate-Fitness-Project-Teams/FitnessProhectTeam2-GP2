using System.Net.Http.Json;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Application.Interfaces;

namespace Elevate.Nutrition.Infrastructure.Services;

public class FceIntegrationService : IFceIntegrationService
{
    private readonly HttpClient _httpClient;

    public FceIntegrationService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<CalorieTargetDto?> GetCalorieTargetAsync(int userId, CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync($"/api/calorie-target/{userId}", ct);

        if (!response.IsSuccessStatusCode)
            return null;

        var envelope = await response.Content.ReadFromJsonAsync<FceResponse>(cancellationToken: ct);

        return envelope?.Data;
    }

    private sealed class FceResponse
    {
        public bool IsSuccess { get; init; }
        public CalorieTargetDto? Data { get; init; }
    }
}
