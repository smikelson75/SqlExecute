using SqlExecute.Engine.Sqlite;
using System.Data.SQLite;

namespace SqlExecute.Tests.Engine.Sqlite.SqliteRepositoryTests
{
    public class SqliteRepositoryTests
    {
        private static SQLiteConnection GetConnection()
        {
            // In-Memory databases, require _connection pooling in order to retain
            // the database through multiple queries. Connection string has been updated
            // to allow _connection pooling for the SQLiteConnection, which manages the pool.
            return new SQLiteConnection(
                    "Data Source=:memory:; Version=3; New=True; Cache Size=100; Journal Mode=Memory;");

        }
        [Fact]
        public async Task CreateRepositoryOpenThenCloseTheConnection() // Make the test method async
        {
            var repository = new SqliteRepository(GetConnection());

            await repository.OpenAsync();
            Assert.True(repository.Connection.State == System.Data.ConnectionState.Open);

            await repository.CloseAsync();
            Assert.True(repository.Connection.State == System.Data.ConnectionState.Closed);
        }

        [Fact]
        public async Task BuildAndExecuteToInsertRows()
        {
            using var repository = new SqliteRepository(GetConnection());

            Assert.Equal(0, await repository.ExecuteNonQueryAsync(@"CREATE TABLE IF NOT EXISTS test (
        id INTEGER PRIMARY KEY AUTOINCREMENT, 
        name TEXT NOT NULL,
        description TEXT)"));

            Assert.Equal(1, await repository.ExecuteNonQueryAsync(
                "INSERT INTO test (name, description) VALUES (\"Jane Doe\",\"A great programmer\")"));
        }

        [Fact]
        public async Task BuildAndExecuteMultipleInserts()
        {
            // In-Memory databases, require _connection pooling in order to retain
            // the database through multiple queries. Connection string has been updated
            // to allow _connection pooling for the SQLiteConnection, which manages the pool.
            using var repository = new SqliteRepository(GetConnection());

            Assert.Equal(0, await repository.ExecuteNonQueryAsync(@"CREATE TABLE IF NOT EXISTS test (
        id INTEGER PRIMARY KEY AUTOINCREMENT, 
        name TEXT NOT NULL,
        description TEXT)"));

            Assert.Equal(2, await repository.ExecuteNonQueryAsync(
                "INSERT INTO test (name, description) VALUES (\"Jane Doe\",\"A great programmer.\")",
                "INSERT INTO test (name, description) VALUES (\"John Doe\", \"Another great programmer\")"));

        }

        [Fact]
        public async Task FailedTaskReturnsInvalidOperationException()
        {
            // In-Memory databases, require _connection pooling in order to retain
            // the database through multiple queries. Connection string has been updated
            // to allow _connection pooling for the SQLiteConnection, which manages the pool.
            var connection = GetConnection();
            using var repository = new SqliteRepository(connection);

            Assert.Equal(0, await repository.ExecuteNonQueryAsync(@"CREATE TABLE IF NOT EXISTS test (
        id INTEGER PRIMARY KEY AUTOINCREMENT, 
        name TEXT NOT NULL,
        description TEXT)"));

            await Assert.ThrowsAsync<AggregateException>(
                async () => await repository.ExecuteNonQueryAsync(
                    "INSERT INTO test (name, description) VALUES (\"Jane Doe\",\"A great programmer\""));
        }

        [Fact]
        public async Task MultipleFailedTasksReturnAggregateExceptionWithInvalidOperationExceptions()
        {
            // In-Memory databases, require _connection pooling in order to retain
            // the database through multiple queries. Connection string has been updated
            // to allow _connection pooling for the SQLiteConnection, which manages the pool.
            var connection = GetConnection();
            using var repository = new SqliteRepository(connection);

            Assert.Equal(0, await repository.ExecuteNonQueryAsync(@"CREATE TABLE IF NOT EXISTS test (
        id INTEGER PRIMARY KEY AUTOINCREMENT, 
        name TEXT NOT NULL,
        description TEXT)"));

            await Assert.ThrowsAsync<AggregateException>(async () => await repository.ExecuteNonQueryAsync(
                "INSERT INTO test (name, description) VALUES (\"Jane Doe\",\"A great programmer.\"",
                "INSERT INTO test (name, description) VALUES (\"John Doe\", \"Another great programmer\""));

        }
    }
}
