namespace SqlExecute.Storage.Yaml.Models
{
    /// <summary>
    /// Represents an action configuration.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        [YamlMember(Alias = "action")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parameters of the action.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        public Dictionary<string, object> Parameters { get; set; } = [];
    }
}
