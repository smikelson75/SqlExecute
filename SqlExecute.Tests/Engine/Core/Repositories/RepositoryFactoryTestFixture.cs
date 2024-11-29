using SqlExecute.Storage.Yaml;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.Repositories
{
    public class RepositoryFactoryTestFixture
    {
        public Configuration Configuration { get; private set; }

        public RepositoryFactoryTestFixture()
        {
            Configuration = ProcessConfiguration.GetConfiguration("config.yaml");
        }
    }
}
