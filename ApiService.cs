using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace BellePOS
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5034/")
            };

            // Figure out how to make and manage api tokens later
            // Optional: global headers
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your_token");
        }

        // Example: Generic GET method
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                // Centralized error handling/logging
                MessageBox.Show($"API error: {ex.Message}");
                return default;
            }
        }

        // Example: Generic POST method
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API error: {ex.Message}");
                return default;
            }
        }
        public async Task<List<T>?> SearchAsync<T>(string endpoint, string queryString = "")
        {
            try
            {
                var fullUrl = string.IsNullOrWhiteSpace(queryString)
                    ? endpoint
                    : $"{endpoint}?{queryString}";

                var response = await _httpClient.GetAsync(fullUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}");
                return null;
            }
        }

    }

}
