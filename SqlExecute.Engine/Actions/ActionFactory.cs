using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Actions.Impementations;
using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories;

namespace SqlExecute.Engine.Actions
{
    public class ActionFactory
    {
        public Dictionary<string, IActionBuilderStrategy> ActionBuilders { get; }
        public RepositoryCollection Repositories { get; }

        public ActionFactory(RepositoryCollection collection)
        {
            ActionBuilders = new Dictionary<string, IActionBuilderStrategy>(StringComparer.OrdinalIgnoreCase);
            Repositories = collection;
        }

        public IAction Build(string name, string type, ActionParameters parameters)
        {
            return type.ToLower() switch
            {
                "nonquery" => NonQueryAction.Create(name, parameters, (connection) => Repositories.Get(connection)),
                _ => throw new ActionNotFoundException(type),
            };
        }
    }
}
