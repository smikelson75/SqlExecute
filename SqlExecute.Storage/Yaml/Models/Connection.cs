namespace SqlExecute.Storage.Yaml.Models
{
    public class Connection
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        [YamlMember(Alias = "connectionString")]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
