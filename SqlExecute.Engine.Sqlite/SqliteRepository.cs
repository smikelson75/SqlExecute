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

        protected async Task<int> ExecuteNonQueryAsync(string query)
        {
            await OpenAsync();

            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = query;
                var result = await command.ExecuteNonQueryAsync();

                return result;
            }
            catch (SQLiteException ex)
            {
                throw new InvalidOperationException($"Unable to execute query against database. ({query})", ex);
            }
        }

        public async Task<int> ExecuteNonQueryAsync(params string[] queries)
        {
            var tasks = new List<Task<int>>();
            var exceptions = new List<Exception>();

            await OpenAsync();

            foreach (var query in queries)
            {
                tasks.Add(
                    Task.Run(async () =>
                    {
                        try
                        {
                            return await ExecuteNonQueryAsync(query);
                        }
                        catch (Exception ex)
                        {
                            lock(exceptions)
                            {
                                exceptions.Add(ex);
                            }

                            return 0;
                        }
                    }));
            }

            int[] results = await Task.WhenAll(tasks);

            if (exceptions.Any())
                throw new AggregateException("One or more processes failed to complete. See inner execptions for details.", exceptions);

            return results.Sum();
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
