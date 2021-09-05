using Httwrap;
using Httwrap.Interface;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace Http.Base;

public class HttpBase<TEntity> : IHttpBase<TEntity>
{
    private IHttwrapClient _httwrapClient;

    protected HttpBase(IConfiguration configuration)
    {
        var baseUrl = configuration.GetConnectionString("MainApi");

        HttpBase<TEntity>.CheckExceptions(async () => SetHttpConnection(baseUrl));
    }

    public void SetHttpConnection(string baseUrl)
    {
        if (!UriIncludesHttpPrefix(baseUrl) && !UriIncludesHttpsPrefix(baseUrl))
        {
            var errorMessage = $"URI is invalid because http(s) was not included in ${baseUrl}";
            throw new ArgumentException(errorMessage, nameof(baseUrl));
        }

        IHttwrapConfiguration httwrapConfiguration = new HttwrapConfiguration(baseUrl);
        _httwrapClient = new HttwrapClient(httwrapConfiguration);
    }

    protected async Task<IHttwrapResponse> GetAsync(
        string urlExtension,
        string token = null) =>
        await HttpBase<TEntity>.CheckExceptions(async () =>
            await _httwrapClient.GetAsync(
                urlExtension,
                null,
                token != null
                    ? GetCustomHeaders(token)
                    : null));

    protected async Task<IHttwrapResponse> PutAsync(
        string urlExtension,
        TEntity model,
        string token = null) =>
        await HttpBase<TEntity>.CheckExceptions(async () =>
            await _httwrapClient.PutAsync(
                urlExtension,
                model,
                null,
                token != null
                    ? GetCustomHeaders(token)
                    : null));

    protected async Task<IHttwrapResponse> PatchAsync(
      string urlExtension,
      TEntity model,
      string token = null) =>
      await HttpBase<TEntity>.CheckExceptions(async () =>
          await _httwrapClient.PatchAsync(
              urlExtension,
              model,
              null,
              token != null
                  ? GetCustomHeaders(token)
                  : null));

    protected async Task<IHttwrapResponse> PostAsync(
        string urlExtension,
        TEntity model,
        string token = null) =>
        await HttpBase<TEntity>.CheckExceptions(async () =>
            await _httwrapClient.PostAsync(
                urlExtension,
                model,
                null,
                token != null
                    ? GetCustomHeaders(token)
                    : null));

    protected async Task<IHttwrapResponse> DeleteAsync(
        string urlExtension,
        string token = null) =>
        await HttpBase<TEntity>.CheckExceptions(async () =>
            await _httwrapClient.DeleteAsync(
                urlExtension,
                null,
                token != null
                    ? GetCustomHeaders(token)
                    : null));

    private static bool UriIncludesHttpPrefix(string uri) => uri.ToLower().Contains("http://");
    private static bool UriIncludesHttpsPrefix(string uri) => uri.ToLower().Contains("https://");

    private static T CheckExceptions<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (SocketException socketException)
        {
            throw new HttpRequestException("Unable to establish connection, socket exception", socketException);
        }
        catch (HttpRequestException httpRequestException)
        {
            throw new HttpRequestException("Unable to establish connection, http request exception",
                httpRequestException);
        }
        catch (HttwrapException httpwrapException)
        {
            throw new HttpRequestException("Unable to establish connection, httwrap exception", httpwrapException);
        }
    }

    private static Dictionary<string, string> GetCustomHeaders(string token) => new() { { "authorization", "Bearer " + token } };
}