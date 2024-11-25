using SqlExecute.Engine.Repositories.Abstractions;
using System.Data.Common;
using System.Data.SQLite;

namespace SqlExecute.Engine.Sqlite
{
    public class SqliteRepositoryBuilder : IRepositoryBuilderStrategy
    {
        public IRepositoryAsync Build(string connectionString)
        {
            var connection = new SQLiteConnection(connectionString);
            return new SqliteRepository(connection);
        }
    }
}
