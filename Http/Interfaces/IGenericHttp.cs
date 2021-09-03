namespace Http;

/// <summary>
///     Standard http access operation methods.
/// </summary>
public interface IGenericHttp<TEntity> : IHttpBase<TEntity>
{
    /// <summary>
    ///     Returns all records.
    /// </summary>
    Task<IEnumerable<TEntity>> GetAllAsync(
        string urlExtension,
        string token = null);

    /// <summary>
    ///     Returns all records with specified ID.
    /// </summary>
    Task<IEnumerable<TEntity>> GetAllAsync(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns all records with specified ID as specified type.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync<T>(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns record by ID.
    /// </summary>
    Task<TEntity> GetAsync(
        string urlExtension,
        string id,
        string token = null);

    /// <summary>
    ///     Returns record by ID as specified type.
    /// </summary>
    Task<T> GetAsync<T>(
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
    Task<string> PostWithResultAsync(
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