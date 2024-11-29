using SqlExecute.Storage.Yaml;
using SqlExecute.Storage.Yaml.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Tests.Engine.Core.Actions
{
    public class ActionParametersTestFixture
    {
        public Configuration Configuration { get; set; }

        public ActionParametersTestFixture()
        {
            Configuration = ProcessConfiguration.GetConfiguration("config.yaml");
        }
    }
}
