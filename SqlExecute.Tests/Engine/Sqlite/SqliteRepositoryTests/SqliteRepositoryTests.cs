using SqlExecute.Engine.Actions;
using SqlExecute.Engine.Actions.Impementations;
using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Sqlite;
using SqlExecute.Storage.Yaml.Models;
using System.Data;
using System.Data.SQLite;

namespace SqlExecute.Tests.Engine.Sqlite.SqliteRepositoryTests
{
    [Collection("SqliteRepositoryTests")]
    public class SqliteRepositoryTests
    {
        private readonly RepositoryCollection _collection;
        private readonly Configuration _configuration;

        public SqliteRepositoryTests(SqliteRepositoryTestFixture fixture)
        {
            _collection = fixture.Collection;
            _configuration = fixture.Configuration;
        }

        [Fact]
        public async Task Open_WhenConnectionIsClosed_ShouldOpenConnection() // Make the test method async
        {
            Assert.NotEmpty(_configuration.Actions);

            Assert.Collection(_configuration.Actions,
                async action => // Make the lambda async
            {
                Assert.Equal("NonQuery", action.Type);

                action.Parameters.TryGetValue("query", out object? query);
                Assert.NotNull(query);

                action.Parameters.TryGetValue("connection", out object? connection);
                Assert.NotNull(connection);

                var queryArray = Assert.IsType<string[]>(query);
                Assert.NotEmpty(queryArray);
                Assert.All(queryArray, q => Assert.IsType<string>(q));

                var factory = new ActionFactory(_collection);
                var parameters = new ActionParameters();
                parameters.AddParameterRange(action.Parameters);
                var nonQueryAction = factory.Build(action.Name, action.Type, parameters);

                await nonQueryAction.ExecuteAsync(); // Await the async method
            });
        }
    }
}
