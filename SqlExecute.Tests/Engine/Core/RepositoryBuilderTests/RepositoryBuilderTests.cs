using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;
using SqlExecute.Engine.Sqlite;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.RepositoryBuilderTests
{
    [Collection("RepositoryBuilderTests")]
    public class RepositoryBuilderTests
    {
        private readonly Configuration _configuration;

        public RepositoryBuilderTests(RepositoryBuilderTestFixture fixture)
        {
            _configuration = fixture.Configuration;
        }

        [Fact]
        public void Build_WhenConfigurationIsProvided_ShouldReturnRepositoryAsync()
        {
            Assert.NotEmpty(_configuration.Connections);

            Assert.Collection(_configuration.Connections,
                connection =>
                {
                    Assert.NotNull(connection.Name);
                    Assert.NotNull(connection.ConnectionString);
                });

            var repositories = new RepositoryCollection();
            var builder = new RepositoryBuilder();
            builder.Register("sqlite", new SqliteRepositoryBuilder());
            foreach (var connection in _configuration.Connections)
            {
                repositories.Add(connection.Name, builder.Build("sqlite", connection.ConnectionString));
            }

            Assert.NotEmpty(repositories);
            Assert.Equal(_configuration.Connections.Length, repositories.Count);
        }
    }
}
