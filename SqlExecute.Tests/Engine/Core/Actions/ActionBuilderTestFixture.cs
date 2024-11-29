using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Sqlite;
using SqlExecute.Engine.Actions;
using SqlExecute.Storage.Yaml;
using SqlExecute.Storage.Yaml.Models;


namespace SqlExecute.Tests.Engine.Core
{
    public class ActionBuilderTestFixture
    {
        public Configuration Configuration { get; private set; }
        public RepositoryCollection Collection { get; private set; }
        public ActionParameters Parameters { get; private set; } = [];

        public ActionBuilderTestFixture()
        {
            Collection = [];
            Configuration = ProcessConfiguration.GetConfiguration("config.yaml");

            foreach (var action in Configuration.Actions)
            {
                foreach (var parameter in action.Parameters)
                {
                    Parameters.AddParameter(parameter.Key, parameter.Value);
                }
            }

            var builder = new RepositoryFactory();
            builder.Register("sqlite", new SqliteRepositoryBuilder());
            foreach (var connection in Configuration.Connections)
            {
                Collection.Add(connection.Name, builder.Build(connection.Provider, connection.ConnectionString));
            }
        }
    }
}
