

namespace P5_Frontend_Car_App.Interfaces;

    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint, CancellationToken ct = default);
        Task<T> PostAsync<T> (string endpoint, object data, CancellationToken ct = default);

        Task PutAsync(string endpoint, object data, CancellationToken ct = default);

        Task DeleteAsync(string endpoint, CancellationToken ct = default);
    }