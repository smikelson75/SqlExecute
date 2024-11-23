using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Exceptions
{
    public class ValidationException(Type classType) : Exception($"Validation of {classType.Name} has failed.")
    {
    }
}
