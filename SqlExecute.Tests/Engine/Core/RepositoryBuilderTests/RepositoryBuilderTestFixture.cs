using SqlExecute.Storage.Yaml;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.RepositoryBuilderTests
{
    public class RepositoryBuilderTestFixture
    {
        public Configuration Configuration { get; private set; }

        public RepositoryBuilderTestFixture()
        {
            Configuration = ProcessConfiguration.GetConfiguration("config.yaml");
        }
    }
}
