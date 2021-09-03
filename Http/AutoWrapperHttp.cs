using Http.Base;
using Http.Models;
using Httwrap;
using Microsoft.Extensions.Configuration;

namespace Http;

public class AutoWrapperHttp<TEntity> : HttpBase<TEntity>, IAutoWrapperHttp<TEntity>
{
    public AutoWrapperHttp(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<AutoWrapperResponseModel<TEntity>>> GetAllAsync(
        string urlExtension,
        string token = null) =>
        (await base.GetAsync(urlExtension, token))
        .ReadAs<IEnumerable<AutoWrapperResponseModel<TEntity>>>();

    public async Task<IEnumerable<AutoWrapperResponseModel<TEntity>>> GetAllAsync(
        string urlExtension,
        string itemId,
        string token = null) =>
        (await base.GetAsync(urlExtension + "/" + itemId, token))
        .ReadAs<IEnumerable<AutoWrapperResponseModel<TEntity>>>();

    public async Task<IEnumerable<AutoWrapperResponseModel<T>>> GetAllAsync<T>(
        string urlExtension,
        string id,
        string token = null) =>
        (await base.GetAsync(urlExtension + "/" + id, token))
        .ReadAs<IEnumerable<AutoWrapperResponseModel<T>>>();

    public async Task<AutoWrapperResponseModel<TEntity>> GetAsync(
        string urlExtension,
        string id,
        string token = null) =>
        (await base.GetAsync(urlExtension + "/" + id, token))
        .ReadAs<AutoWrapperResponseModel<TEntity>>();

    public async Task<AutoWrapperResponseModel<T>> GetAsync<T>(
        string urlExtension,
        string id,
        string token = null) =>
        (await base.GetAsync(urlExtension + "/" + id, token))
        .ReadAs<AutoWrapperResponseModel<T>>();

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

    public async Task<AutoWrapperResponseModel<string>> PostWithResultAsync(
        string urlExtension,
        TEntity model,
        string token = null) =>
        (await base.PostAsync(urlExtension, model, token))
        .ReadAs<AutoWrapperResponseModel<string>>();

    public async Task<bool> DeleteAsync(
        string urlExtension,
        string id,
        string token = null) =>
        (await base.DeleteAsync(urlExtension + "/" + id, token))
        .Success;
}