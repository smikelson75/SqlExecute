using SqlExecute.Engine.Exceptions;
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
        }

        [Fact]
        public void MissingConfigurationFileThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => ProcessConfiguration.GetConfiguration("missing.yaml"));
        }

        [Fact]
        public void EmptyConfigurationFileThrowsValidationException()
        {
            var reader = GetReader(string.Empty);
            Assert.Throws<ValidationException>(() => ProcessConfiguration.GetConfiguration(reader));
        }

        [Fact]
        public void InvalidConfigurationVersionThrowsValidationException()
        {
            var reader = GetReader(
                @"version: something.else
connections:
- name: local
  connectionString: sqlite:///SqlExecute/db.sqlite3

actions:
- action: NonQuery
  name: Create table
  parameters:
    connection: local
    queries: 
    - |
      CREATE TABLE IF NOT EXISTS test (
        id INTEGER PRIMARY KEY AUTOINCREMENT, 
        name TEXT NOT NULL,
        description TEXT
      ) ");
            Assert.Throws<ValidationException>(() => ProcessConfiguration.GetConfiguration(reader));
        }

        [Fact]
        public void InvalidConfigurationNoActionsThrowsValidationException()
        {
            var reader = GetReader(
                @"version: 1.0.0

connections:
- name: local
  connectionString: sqlite:///SqlExecute/db.sqlite3
");
            Assert.Throws<ValidationException>(() => ProcessConfiguration.GetConfiguration(reader));
        }

        private static StreamReader GetReader(string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}