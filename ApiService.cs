using System.Net.Http;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService()
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri("http://localhost:5294/api/");
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var res = await _http.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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