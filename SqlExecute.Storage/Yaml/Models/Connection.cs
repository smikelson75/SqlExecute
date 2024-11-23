namespace SqlExecute.Storage.Yaml.Models
{
    /// <summary>
    /// Represents a connection configuration.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets or sets the name of the connection.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        [YamlMember(Alias = "connectionString")]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
