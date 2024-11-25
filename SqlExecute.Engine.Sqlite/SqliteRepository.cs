using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Engine.Sqlite
{

    public class SqliteRepository(SQLiteConnection connection) : IRepositoryAsync
    {
        private readonly SQLiteConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        private bool _disposed = false;

        public DbConnection Connection => _connection as DbConnection;

        public async Task OpenAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            await OpenAsync();

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            var result = await command.ExecuteNonQueryAsync();

            await CloseAsync();
            return result;
        }

        public async Task CloseAsync()
        {
            if (_connection.State == ConnectionState.Open)
                await _connection.CloseAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
