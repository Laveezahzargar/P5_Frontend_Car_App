

namespace P5_Frontend_Car_App.Interfaces;

    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task PostAsync (string url, object data);

        Task PutAsync(string url, object data);

        Task DeleteAsync(string url);
    }