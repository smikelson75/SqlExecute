using SqlExecute.Engine.Exceptions;
using SqlExecute.Storage.Yaml.Models.Validators;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Validator = SqlExecute.Engine.Validator;

namespace SqlExecute.Storage.Yaml
{
    /// <summary>
    /// Provides methods to process and validate configuration files.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <see cref="ProcessConfiguration"/> class:
    /// <code>
    /// var configurationPath = "path/to/configuration.yaml";
    /// try
    /// {
    ///     var configuration = ProcessConfiguration.GetConfiguration(configurationPath);
    ///     Console.WriteLine("Configuration loaded successfully.");
    /// }
    /// catch (Exception ex)
    /// {
    ///     Console.WriteLine($"Failed to load configuration: {ex.Message}");
    /// }
    /// </code>
    /// </example>
    public static class ProcessConfiguration
    {
        /// <summary>
        /// Gets the configuration from the specified file path.
        /// </summary>
        /// <param name="configurationPath">The path to the configuration file.</param>
        /// <returns>The deserialized and validated <see cref="Configuration"/> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the configuration path is null or whitespace.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the configuration file is not found.</exception>
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

        /// <summary>
        /// Gets the configuration from the specified <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="StreamReader"/> to read the configuration from.</param>
        /// <returns>The deserialized and validated <see cref="Configuration"/> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the reader is null.</exception>
        /// <exception cref="ValidationException">Thrown when the deserialization fails.</exception>
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
