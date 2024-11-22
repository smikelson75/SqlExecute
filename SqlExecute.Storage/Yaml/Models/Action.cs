using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Storage.Yaml.Models
{
    public class Action
    {
        [YamlMember(Alias = "action")]
        public string Type { get; set; } = string.Empty;

        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;
    }
}
