using EcfrAnalyzer.Models;
using System.Text.Json;

namespace EcfrAnalyzer.Services
{
    public interface IEcfrApiService
    {
        Task<AgenciesResponseModel> GetAgenciesAsync(CancellationToken cancellationToken = default);
        Task<CorrectionsResponseModel> GetCorrectionsAsync(CancellationToken cancellationToken = default);
        Task<SearchResultsModel> GetSearchResultsAsync(string query, CancellationToken cancellationToken = default);
    }
    public class EcfrApiService : IEcfrApiService
    {
        private readonly HttpClient _httpClient;
        private AgenciesResponseModel? _cachedAgencies;
        private DateTime _lastRefresh = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromHours(24);

        private CorrectionsResponseModel? _cachedCorrections;
        private DateTime _lastCorrectionsRefresh = DateTime.MinValue;

        public EcfrApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AgenciesResponseModel> GetAgenciesAsync(CancellationToken cancellationToken = default)
        {
            if (_cachedAgencies != null && DateTime.UtcNow - _lastRefresh < _cacheExpiration)
            {
                return _cachedAgencies;
            }

            try
            {
                var response = await _httpClient.GetAsync("admin/v1/agencies.json", cancellationToken);
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                _cachedAgencies = await JsonSerializer.DeserializeAsync<AgenciesResponseModel>(contentStream, cancellationToken: cancellationToken);
                _lastRefresh = DateTime.UtcNow;

                return _cachedAgencies ?? new AgenciesResponseModel();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP request failed while fetching agencies: {ex.Message}");
                return new AgenciesResponseModel();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse agencies JSON: {ex.Message}");
                return new AgenciesResponseModel();
            }
            catch (TaskCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Request timeout while fetching agencies: {ex.Message}");
                return new AgenciesResponseModel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error fetching agencies: {ex.Message}");
                return new AgenciesResponseModel();
            }
        }

        public async Task<CorrectionsResponseModel> GetCorrectionsAsync(CancellationToken cancellationToken = default)
        {
            if (_cachedCorrections != null && DateTime.UtcNow - _lastCorrectionsRefresh < _cacheExpiration)
            {
                return _cachedCorrections;
            }

            try
            {
                var response = await _httpClient.GetAsync("admin/v1/corrections.json", cancellationToken);
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                _cachedCorrections = await JsonSerializer.DeserializeAsync<CorrectionsResponseModel>(contentStream, cancellationToken: cancellationToken);
                _lastCorrectionsRefresh = DateTime.UtcNow;

                return _cachedCorrections ?? new CorrectionsResponseModel();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP request failed while fetching corrections: {ex.Message}");
                return new CorrectionsResponseModel();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse corrections JSON: {ex.Message}");
                return new CorrectionsResponseModel();
            }
            catch (TaskCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Request timeout while fetching corrections: {ex.Message}");
                return new CorrectionsResponseModel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error fetching corrections: {ex.Message}");
                return new CorrectionsResponseModel();
            }
        }

        /// <summary>
        /// This API endpoint is way too big to completely build out given the 1200 code line restriction. I would also need more information about the models to do so. 
        /// There is a ton I could do with pagination but I would need to know more about the users use case to do it correctly. 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SearchResultsModel> GetSearchResultsAsync(string query, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"search/v1/results?query={Uri.EscapeDataString(query)}&agency_slugs%5B%5D=agriculture-department&per_page=1000&page=1&order=relevance&paginate_by=results", cancellationToken);
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var searchResults = await JsonSerializer.DeserializeAsync<SearchResultsModel>(contentStream, cancellationToken: cancellationToken);
                return searchResults ?? new SearchResultsModel();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP request failed while fetching search results: {ex.Message}");
                return new SearchResultsModel();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse search results JSON: {ex.Message}");
                return new SearchResultsModel();
            }
            catch (TaskCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Request timeout while fetching search results: {ex.Message}");
                return new SearchResultsModel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error fetching search results: {ex.Message}");
                return new SearchResultsModel();
            }
        }
    }
}
