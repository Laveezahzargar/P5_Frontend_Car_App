
using System.Net.Http;
using P5_Frontend_Car_App.Interfaces;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using P5_Frontend_Car_App.DTOs;
using System;
using System.Collections.Generic;

namespace P5_Frontend_Car_App.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient httpClient)
    {
        _http = httpClient;
        _http.BaseAddress = new Uri("http://localhost:5294/api/");
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var res = await _http.GetAsync(url);

        if (!res.IsSuccessStatusCode)
        {
            var error = await res.Content.ReadAsStringAsync();

            throw new Exception($"API Error: {res.StatusCode} - {error}");
        }

        var json = await res.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiResponse<T>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return result!.Data!;
    }

    public async Task PostAsync(string url, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await _http.PostAsync(url, content);
    }

    public async Task PutAsync(string url, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await _http.PutAsync(url, content);
    }

    public async Task DeleteAsync(string url)
    {
        var response = await _http.DeleteAsync(url);

        response.EnsureSuccessStatusCode();
    }
}