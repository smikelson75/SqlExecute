using SqlExecute.Storage.Yaml.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SqlExecute.Storage.Yaml
{
    public static class ProcessConfiguration
    {
        public static Configuration GetConfiguration(string configurationPath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(configurationPath);

            if (!File.Exists(configurationPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configurationPath}");
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            using var reader = new StreamReader(configurationPath);

            var configuration = deserializer.Deserialize<Configuration>(reader);
            return configuration;
        }
    }
}
