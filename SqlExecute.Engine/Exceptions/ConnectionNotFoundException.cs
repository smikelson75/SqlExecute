using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Exceptions
{
    public class ConnectionNotFoundException(string key) : Exception($"Unable to find connection \"{key}\".")
    {
    }
}
