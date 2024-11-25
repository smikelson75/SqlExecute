using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Sqlite;
using SqlExecute.Storage.Yaml;
using SqlExecute.Storage.Yaml.Models;


namespace SqlExecute.Tests.Engine.Core.ActionBuilderTests
{
    public class ActionBuilderTestFixture
    {
        public Configuration Configuration { get; set; }
        public RepositoryCollection Collection { get; set; }

        public ActionBuilderTestFixture()
        {
            Collection = new RepositoryCollection();
            Configuration = ProcessConfiguration.GetConfiguration("config.yaml");

            var builder = new RepositoryBuilder();
            builder.Register("sqlite", new SqliteRepositoryBuilder());
            foreach (var connection in Configuration.Connections)
            {
                Collection.Add(connection.Name, builder.Build(connection.Provider, connection.ConnectionString));
            }
        }
    }
}
