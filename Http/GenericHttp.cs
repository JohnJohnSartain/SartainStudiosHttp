using Http.Base;
using Httwrap;
using Microsoft.Extensions.Configuration;

namespace Http;

public class GenericHttp<TEntity> : HttpBase<TEntity>, IGenericHttp<TEntity>
{
    public GenericHttp(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        string urlExtension,
        string token = null) =>
            (await base.GetAsync(urlExtension, token))
                .ReadAs<IEnumerable<TEntity>>();

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        string urlExtension,
        string itemId,
        string token = null) =>
            (await base.GetAsync(urlExtension + "/" + itemId, token))
                .ReadAs<IEnumerable<TEntity>>();

    public async Task<IEnumerable<T>> GetAllAsync<T>(
        string urlExtension,
        string id,
        string token = null) =>
            (await base.GetAsync(urlExtension + "/" + id, token))
                .ReadAs<IEnumerable<T>>();

    public async Task<TEntity> GetAsync(
        string urlExtension,
        string id,
        string token = null) =>
            (await base.GetAsync(urlExtension + "/" + id, token))
                .ReadAs<TEntity>();

    public async Task<T> GetAsync<T>(
        string urlExtension,
        string id,
        string token = null) =>
            (await base.GetAsync(urlExtension + "/" + id, token))
                .ReadAs<T>();

    public async Task<bool> PutAsync(
        string urlExtension,
        string id,
        TEntity model,
        string token = null) =>
            (await base.PutAsync(urlExtension + "/" + id, model, token))
                .Success;

    public new async Task<bool> PostAsync(
        string urlExtension,
        TEntity model,
        string token = null) =>
            (await base.PostAsync(urlExtension, model, token))
                .Success;

    public async Task<string> PostWithResultAsync(
        string urlExtension,
        TEntity model,
        string token = null) =>
            (await base.PostAsync(urlExtension, model, token))
                .ReadAs<string>();

    public async Task<bool> DeleteAsync(
        string urlExtension,
        string id,
        string token = null) =>
            (await base.DeleteAsync(urlExtension + "/" + id, token))
                .Success;
}