using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Actions.Abstractions
{
    public interface IAction
    {
        string Name { get; }
        ActionStatus Status { get; }
        Task<int> ExecuteAsync();

    }
}
