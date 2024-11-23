using SqlExecute.Engine.Exceptions;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Validator = SqlExecute.Engine.Validator;

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

            using var reader = new StreamReader(configurationPath);

            return GetConfiguration(reader);
        }

        public static Configuration GetConfiguration(StreamReader reader)
        {
            ArgumentNullException.ThrowIfNull(reader);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var configuration = deserializer.Deserialize<Configuration>(reader) 
                ?? throw new ValidationException(typeof(Configuration));

            Validator.Validate<ConfigurationValidator, Configuration>(configuration);

            return configuration;
        }
    }
}
