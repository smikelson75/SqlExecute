using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Actions.Abstractions
{
    public interface IAction
    {
        Task<int> ExecuteAsync();
    }
}
