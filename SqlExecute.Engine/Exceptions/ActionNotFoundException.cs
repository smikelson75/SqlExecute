using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Exceptions
{
    public class ActionNotFoundException(string key) : Exception($"Action not found with key \"{key}\".")
    {
    }
}
