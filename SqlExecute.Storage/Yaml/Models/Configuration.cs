namespace SqlExecute.Storage.Yaml.Models
{
    public class Configuration
    {
        [YamlMember(Alias = "version")]
        public string Version { get; set; } = string.Empty;

        [YamlMember(Alias = "connections")]
        public Connection[] Connections { get; set; } = [];

        [YamlMember(Alias = "actions")]
        public Action[] Actions { get; set; } = [];
    }
}
