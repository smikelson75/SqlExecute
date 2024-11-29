using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;
using SqlExecute.Engine.Sqlite;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.Repositories
{
    [Collection("RepositoryFactoryTests")]
    public class RepositoryFactoryTests
    {
        private readonly Configuration _configuration;

        public RepositoryFactoryTests(RepositoryFactoryTestFixture fixture)
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

            var repositories = new SqlExecute.Engine.Repositories.RepositoryCollection();
            var builder = new RepositoryFactory();
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
