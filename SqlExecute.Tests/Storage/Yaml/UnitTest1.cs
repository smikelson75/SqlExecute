using SqlExecute.Storage.Yaml;

namespace SqlExecute.Tests.Storage.Yaml
{
    public class YamlConfigurationTests
    {
        [Fact]
        public void ValidConfigurationHasBeenParsed()
        {
            var configuration = ProcessConfiguration.GetConfiguration("config.yaml");

            Assert.NotNull(configuration);
            Assert.Equal("1.0.0", configuration.Version);
            Assert.NotNull(configuration.Connections);

            Assert.NotEmpty(configuration.Connections);
            Assert.Collection(configuration.Connections, connection =>
            {
                Assert.Equal("local", connection.Name);
                Assert.Equal("sqlite:///SqlExecute/db.sqlite3", connection.ConnectionString);
            });

            Assert.NotEmpty(configuration.Actions);
            Assert.Collection(configuration.Actions, action =>
            {
                Assert.Equal("NonQuery", action.Type);
                Assert.Equal("Create table", action.Name);
            },
            action =>
            {
                Assert.Equal("TableLoad", action.Type);
                Assert.Equal("Load table", action.Name);
            });
        }

        [Fact]
        public void MissingConfigurationFileThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => ProcessConfiguration.GetConfiguration("missing.yaml"));
        }
    }
}