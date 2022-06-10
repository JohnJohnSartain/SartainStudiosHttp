using SartainStudios.SharedModels.Http;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SartainStudios.Http;

public interface IHttpClient
{
    Task<ResponseModel<TEntity>> GetAsync<TEntity>(string requestUri, string token);
    Task<ResponseModel<string>> PutAsync<TEntity>(string requestUri, TEntity entity, string token);
    Task<ResponseModel<string>> PostAsync<TEntity>(string requestUri, TEntity entity, string token);
    Task<ResponseModel<string>> DeleteAsync(string requestUri, string token);

    void SetConnection(string baseUrl, string token);
}

public class HttpClient : IHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public HttpClient(System.Net.Http.HttpClient httpClient) => _httpClient = httpClient;

    public void SetConnection(string baseUrl, string token)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("Connection string was missing");

        _httpClient.BaseAddress = new Uri(baseUrl);

        SetToken(token);
    }

    public Task<ResponseModel<TEntity>> GetAsync<TEntity>(string requestUri, string token)
    {
        SetToken(token);
        return _httpClient.GetFromJsonAsync<ResponseModel<TEntity>>(_httpClient.BaseAddress + requestUri);
    }

    public async Task<ResponseModel<string>> PutAsync<TEntity>(string requestUri, TEntity entity, string token)
    {
        SetToken(token);
        return await (await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + requestUri, entity)).Content.ReadFromJsonAsync<ResponseModel<string>>();
    }

    public async Task<ResponseModel<string>> PostAsync<TEntity>(string requestUri, TEntity entity, string token)
    {
        SetToken(token);
        return await (await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + requestUri, entity)).Content.ReadFromJsonAsync<ResponseModel<string>>();
    }

    public async Task<ResponseModel<string>> DeleteAsync(string requestUri, string token)
    {
        SetToken(token);
        return await (await _httpClient.DeleteAsync(_httpClient.BaseAddress + requestUri)).Content.ReadFromJsonAsync<ResponseModel<string>>();
    }

    private void SetToken(string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}