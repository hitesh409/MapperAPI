using System.Data;

namespace MapperAPI.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        Task<DataTable> GetDataTableAsync(string sql, object? parameters = null);
        Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object? parameters = null);
        Task<T?> GetValueFromTableAsync<T>(string sql, object? parameters = null);
        Task<IEnumerable<T>> ExecuteStoreProcedureAsync<T>(string storeProcedure, object? parameters = null);
        Task<int> UpdateDataTableAsync(string sql, object? parameters = null);

    }
}
