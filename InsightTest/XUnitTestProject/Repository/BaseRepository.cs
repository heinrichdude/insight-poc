using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace XUnitTestProject.Repository
{
    public abstract class BaseRepository
    {
        private readonly SqlConnectionStringBuilder _database;
        public readonly SqlConnection _connection;

        public BaseRepository(string databaseConnectionString)
        {
            _database = new SqlConnectionStringBuilder(databaseConnectionString);
            _connection = _database.Open();
        }

        public BaseRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }

        public IList<FastExpando> QuerySql(string query)
        {
            return _connection.QuerySql(query);
        }

        public abstract string SelectSingle { get; }
    }
}
