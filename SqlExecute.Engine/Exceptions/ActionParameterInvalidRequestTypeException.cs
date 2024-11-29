using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Exceptions
{
    public class ActionParameterInvalidRequestTypeException(Type expected, Type received) : Exception($"When requesting {expected.Name}, {received.Name} was actually stored.")
    {
    }
}
