namespace SqlExecute.Storage.Yaml.Models
{
    /// <summary>
    /// Represents a configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets the version of the configuration.
        /// </summary>
        [YamlMember(Alias = "version")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the connections in the configuration.
        /// </summary>
        [YamlMember(Alias = "connections")]
        public Connection[] Connections { get; set; } = [];

        /// <summary>
        /// Gets or sets the actions in the configuration.
        /// </summary>
        [YamlMember(Alias = "actions")]
        public Action[] Actions { get; set; } = [];
    }
}
