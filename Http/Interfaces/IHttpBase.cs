namespace Http;

public interface IHttpBase<TEntity>
{
    void SetHttpConnection(string baseUrl);
}