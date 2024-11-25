using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Collections;

namespace SqlExecute.Engine.Repositories
{
    public class RepositoryCollection : IEnumerable<IRepositoryAsync>
    {
        private readonly Dictionary<string, IRepositoryAsync> _repositories = new(StringComparer.OrdinalIgnoreCase);

        public void Add(string key, IRepositoryAsync connection)
        {
            ArgumentNullException.ThrowIfNull(connection);
            ArgumentNullException.ThrowIfNullOrEmpty(key);

            if (_repositories.ContainsKey(key))
            {
                throw new ConnectionAlreadyExistsException($"Connection with key {key} already exists.");
            }

            _repositories.Add(key.ToLower(), connection);
        }

        public IRepositoryAsync Get(string key)
        {
            if (!_repositories.ContainsKey(key))
            {
                throw new ConnectionNotFoundException($"Connection with key {key} not found.");
            }

            return _repositories[key];
        }

        public IEnumerator<IRepositoryAsync> GetEnumerator()
        {
            return _repositories.Values.GetEnumerator();
        }

        public void Remove(string key)
        {
            if (!_repositories.ContainsKey(key))
            {
                throw new ConnectionNotFoundException($"Connection with key {key} not found.");
            } 

            _repositories.Remove(key);
        }

        public int Count => _repositories.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _repositories.GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return _repositories.ContainsKey(key);
        }
    }
}
