using Dapper;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;


namespace MapperAPI.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string? _connetionString;

        public DbConnectionFactory() {
            try
            {
                Env.Load();
                _connetionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? throw new Exception("CONNECTION_STRING environment variable not set!");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connetionString);
        }

        /// <summary>
        /// Fetches a list of strongly-typed objects from the database.
        /// </summary>
        public async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, parameters);
        }


        /// <summary>
        /// Executes an inline SQL query that returns a single scalar value.
        /// </summary>
        public async Task<T?> GetValueFromTableAsync<T>(string sql, object? parameters = null)
        {
            using var con = CreateConnection();
            return await con.ExecuteScalarAsync<T>(sql, parameters);
        }

        /// <summary>
        /// Executes a stored procedure. Can return either a DataTable or scalar depending on query.
        /// </summary>
        public async Task<IEnumerable<T>> ExecuteStoreProcedureAsync<T>(string storedProcedure,object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Executes an inline update/delete query and returns number of rows affected.
        /// </summary>
        public async Task<int> UpdateDataTableAsync(string sql, object? parameters = null)
        {
            using var con = CreateConnection();
            return await con.ExecuteAsync(sql, parameters, commandType:CommandType.Text);
        }

        /// <summary>
        /// Executes a stored procedure that returns a single scalar value. (Insert, Update, Delete operations)
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, object? parametes = null)
        {
            using var con = CreateConnection();
            return await con.ExecuteAsync(storedProcedure,parametes, commandType:CommandType.StoredProcedure);
        }

        
    }
}
