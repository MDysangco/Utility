using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Utils
{
    public class HttpHelper
    {

        public static async Task<string?> MakeRequest(HttpMethod method, string url, bool secure)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage? request = new HttpRequestMessage(method, url);
                HttpResponseMessage response = await client.SendAsync(request);

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

        public static async Task<string?> MakePost(string url, string jsonBody, bool secure)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage? request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
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

        public static async Task<T?> MakeRequest<T>(HttpMethod method, string url, bool secure)
        {
            string? json = await MakeRequest(method, url, secure);

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

        public static async Task<T?> MakePost<T>(string url, string jsonBody, bool secure)
        {
            string? json = await MakePost(url, jsonBody, secure);

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

    }
}
