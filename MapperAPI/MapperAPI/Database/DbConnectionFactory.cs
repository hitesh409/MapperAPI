using Dapper;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;


namespace MapperAPI.Database
{
    public class DbConnectionFactory:IDbConnectionFactory
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
        /// Executes an inline SQL query and returns results in a DataTable.
        /// </summary>
        public async Task<DataTable> GetDataTableAsync(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            using var reader = await connection.ExecuteReaderAsync(sql, parameters,commandType:CommandType.Text);
            var table = new DataTable();
            table.Load(reader);
            return table;
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
        public async Task<DataTable> ExecuteStoreProcedureAsync(string storeProcedure,object? parameters = null)
        {
            using var con = CreateConnection();
            using var reader = await con.ExecuteReaderAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        /// <summary>
        /// Executes an inline update/delete query and returns number of rows affected.
        /// </summary>
        public async Task<int> UpdateDataTableAsync(string sql, object? parameters = null)
        {
            using var con = CreateConnection();
            return await con.ExecuteAsync(sql, parameters, commandType:CommandType.Text);
        }
    }
}
