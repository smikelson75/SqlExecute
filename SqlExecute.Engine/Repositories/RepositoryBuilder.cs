using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Data.Common;

namespace SqlExecute.Engine.Repositories
{
    public class RepositoryBuilder
    {
        private readonly Dictionary<string, IRepositoryBuilderStrategy> _strategies = new(StringComparer.OrdinalIgnoreCase);

        public void Register(string key, IRepositoryBuilderStrategy strategy)
        {
            if (_strategies.ContainsKey(key))
            {
                throw new ConnectionAlreadyExistsException($"Connection builder strategy with key {key} already exists.");
            }

            _strategies.Add(key, strategy);
        }

        public IRepositoryAsync Build(string key, string connectionString)
        {
            if (!_strategies.ContainsKey(key))
            {
                throw new ConnectionNotFoundException($"Connection builder strategy with key {key} not found.");
            }

            return _strategies[key].Build(connectionString);
        }
    }
}
