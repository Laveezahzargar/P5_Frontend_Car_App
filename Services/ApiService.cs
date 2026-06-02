using Microsoft.Extensions.Configuration;
using P5_Frontend_Car_App.DTOs;
using P5_Frontend_Car_App.Interfaces;
using Serilog;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace P5_Frontend_Car_App.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _http = httpClient;
        _http.BaseAddress = new Uri(config["ApiBaseUri"]);
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken ct=default)
    {
        _http.DefaultRequestHeaders.Remove("Authorization");

        if (!string.IsNullOrEmpty(Session.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session.Token);
        }

        Log.Information("GET Request: {Endpoint}", endpoint);
        var res = await _http.GetAsync(endpoint,ct);

        var json = await res.Content.ReadAsStringAsync(ct);

        if (!res.IsSuccessStatusCode)
        {
            Log.Error("GET Error: {Endpoint} | Status: {Status} | Body: {Body}",
             endpoint, res.StatusCode, json);

            throw new Exception($"API Error: {res.StatusCode} - {json}");
        }

        var result = JsonSerializer.Deserialize<T>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        Log.Information("GET Success: {Endpoint} | Status: {Status}",
    endpoint, res.StatusCode);

        return result!;
    }

    public async Task<T> PostAsync<T>(string endpoint, object data, CancellationToken ct=default)
    {
        _http.DefaultRequestHeaders.Remove("Authorization");

        if (!string.IsNullOrEmpty(Session.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session.Token);
        }

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        Log.Information("POST {Endpoint} | Payload: {Payload}", endpoint, json);

        var response = await _http.PostAsync(endpoint, content, ct);

        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            Log.Error("POST Error: {Endpoint} | Status: {Status} | Body: {Body}",
            endpoint, response.StatusCode, responseBody);
            throw new Exception($"API Error: {response.StatusCode} - {responseBody}");
        }

        var result = JsonSerializer.Deserialize<T>(
            responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        Log.Information("POST Success: {Endpoint} | Status: {Status}",
    endpoint, response.StatusCode);

        return result!;
    }

    public async Task PutAsync(string endpoint, object data, CancellationToken ct = default)
    {
        _http.DefaultRequestHeaders.Remove("Authorization");

        if (!string.IsNullOrEmpty(Session.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session.Token);
        }

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        Log.Information("PUT {Endpoint} | Payload: {Payload}", endpoint, json);

        var response = await _http.PutAsync(endpoint, content, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            Log.Error("PUT Error: {Endpoint} | Status: {Status} | Body: {Body}",
                endpoint, response.StatusCode, responseBody);

            throw new Exception($"API Error: {response.StatusCode} - {responseBody}");
        }

        Log.Information("PUT Success: {Endpoint} | Status: {Status}",
            endpoint, response.StatusCode);
    }

    public async Task DeleteAsync(string endpoint, CancellationToken ct = default)
    {
        Log.Information("DELETE Request: {Endpoint}", endpoint);

        _http.DefaultRequestHeaders.Remove("Authorization");

        if (!string.IsNullOrEmpty(Session.Token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session.Token);
        }

        var response = await _http.DeleteAsync(endpoint, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            Log.Error("DELETE Error: {Endpoint} | Status: {Status} | Body: {Body}",
                endpoint, response.StatusCode, responseBody);

            throw new Exception($"API Error: {response.StatusCode} - {responseBody}");
        }

        Log.Information("DELETE Success: {Endpoint} | Status: {Status}",
            endpoint, response.StatusCode);
    }
}