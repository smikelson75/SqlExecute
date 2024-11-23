using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine
{
    public class NonQueryOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Connection { get; set; } = string.Empty;
        public string[] Queries { get; set; } = Array.Empty<string>();
    }
}
