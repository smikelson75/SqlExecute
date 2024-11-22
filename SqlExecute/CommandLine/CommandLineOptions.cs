using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.CommandLine
{
    internal class CommandLineOptions
    {
        [Value(0, MetaName = "config", HelpText = "Path to the configuration file.", Required = true)]
        internal string ConfigurationPath { get; set; } = string.Empty;
    }
}
