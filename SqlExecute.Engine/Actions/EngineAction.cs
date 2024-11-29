using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Engine.Actions
{
    public class EngineAction
    {
        public RepositoryCollection Connections { get; private set; } = null!;
        public ActionCollection Actions { get; } = null!;
    }

    public enum ActionTypes
    {
        NonQuery,
        TableLoad,
    }
}
