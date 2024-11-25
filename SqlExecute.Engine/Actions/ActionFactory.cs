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

        public void Register(string key, IActionBuilderStrategy strategy)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            ArgumentNullException.ThrowIfNull(strategy);

            if (ActionBuilders.ContainsKey(key))
            {
                throw new ActionAlreadyExistsException($"Action builder strategy with key {key} already exists.");
            }

            ActionBuilders[key] = strategy;
        }

        public IAction Build(string name, string type, ActionParameters parameters)
        {
            switch (type)
            {
                case "nonquery":

                    return NonQueryAction.Create(name, parameters, (connection) =>
                    {
                        if (Repositories.ContainsKey(connection))
                            return Repositories.Get(connection);

                        throw new ConnectionNotFoundException(connection);
                    });

                default:
                    throw new ActionNotFoundException(type);
            }
        }
    }
}
