using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Exceptions
{
    public class ActionAlreadyExistsException(string key) : Exception($"Action already exists with key \"{key}\".")
    {
    }
}
