using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Utils
{
    public class HttpHelper
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string?> MakeRequest(HttpMethod method, string url, string token)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(method, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Unable to make request: {response.StatusCode}");
                }
                return default;
            }
        }

        public static async Task<string?> MakePost(string url, string jsonBody, string token)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Unable to make request: {response.StatusCode}");
                }
                return default;
            }
        }

        public static async Task<T?> MakeRequest<T>(HttpMethod method, string url, string token)
        {
            string? json = await MakeRequest(method, url, token);

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

        public static async Task<T?> MakePost<T>(string url, string jsonBody, string token)
        {
            string? json = await MakePost(url, jsonBody, token);

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

    }
}
