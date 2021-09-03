using Http.Models;

namespace Http;

/// <summary>
///     Standard http access operation methods which are read through AutoWrapper.
/// </summary>
public interface IAutoWrapperHttp<TEntity> : IHttpBase<TEntity>
{
    /// <summary>
    ///     Returns all records.
    /// </summary>
    Task<IEnumerable<AutoWrapperResponseModel<TEntity>>> GetAllAsync(
        string urlExtension,
        string token = null);

    /// <summary>
    ///     Returns all records with specified ID.
    /// </summary>
    Task<IEnumerable<AutoWrapperResponseModel<TEntity>>> GetAllAsync(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns all records with specified ID as specified type.
    /// </summary>
    Task<IEnumerable<AutoWrapperResponseModel<T>>> GetAllAsync<T>(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns record by ID.
    /// </summary>
    Task<AutoWrapperResponseModel<TEntity>> GetAsync(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns record by ID as specified type.
    /// </summary>
    Task<AutoWrapperResponseModel<T>> GetAsync<T>(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Update record by ID.
    /// </summary>
    Task<bool> PutAsync(
        string urlExtension,
        string id,
        TEntity model,
        string token = null);

    /// <summary>
    ///     Create record.
    /// </summary>
    Task<bool> PostAsync(
        string urlExtension,
        TEntity model,
        string token = null);

    /// <summary>
    ///     Create record and return data.
    /// </summary>
    Task<AutoWrapperResponseModel<string>> PostWithResultAsync(
        string urlExtension,
        TEntity model,
        string token = null);

    /// <summary>
    ///     DeleteRecordById.
    /// </summary>
    Task<bool> DeleteAsync(
        string urlExtension,
        string id,
        string token = null);
}