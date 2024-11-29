using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Collections;

namespace SqlExecute.Engine.Repositories
{
    /// <summary>
    /// Represents a collection of repository instances that can be accessed by a key.
    /// </summary>
    /// <example>
    /// <code>
    /// using SqlExecute.Engine.Repositories;
    /// using SqlExecute.Engine.Repositories.Abstractions;
    /// using System.Data.Common;
    /// 
    /// public class SqlRepository : IRepositoryAsync
    /// {
    ///     public DbConnection Connection { get; private set; }
    /// 
    ///     public SqlRepository(string connectionString)
    ///     {
    ///         // Initialize the connection
    ///     }
    /// 
    ///     public Task OpenAsync()
    ///     {
    ///         // Implementation for opening the connection
    ///     }
    /// 
    ///     public Task CloseAsync()
    ///     {
    ///         // Implementation for closing the connection
    ///     }
    /// 
    ///     public Task<int> ExecuteNonQueryAsync(string query)
    ///     {
    ///         // Implementation for executing a non-query SQL command
    ///     }
    /// 
    ///     public void Dispose()
    ///     {
    ///         // Implementation for disposing the repository
    ///     }
    /// }
    /// 
    /// public class RepositoryCollectionExample
    /// {
    ///     public void RunExample()
    ///     {
    ///         var collection = new RepositoryCollection();
    ///         var repository = new SqlRepository("your-connection-string");
    /// 
    ///         // Add the repository to the collection
    ///         collection.Add("sql", repository);
    /// 
    ///         // Retrieve the repository from the collection
    ///         var retrievedRepository = collection.Get("sql");
    /// 
    ///         // Use the repository
    ///     }
    /// }
    /// </code>
    /// </example>
    public class RepositoryCollection : IEnumerable<IRepositoryAsync>
    {
        private readonly Dictionary<string, IRepositoryAsync> _repositories = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Adds a repository to the collection with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with the repository.</param>
        /// <param name="connection">The repository to add.</param>
        /// <exception cref="RepositoryAlreadyExistsException">Thrown when a repository with the specified key already exists.</exception>
        public void Add(string key, IRepositoryAsync connection)
        {
            ArgumentNullException.ThrowIfNull(connection);
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            if (_repositories.ContainsKey(key))
            {
                throw new RepositoryAlreadyExistsException($"Connection with key {key} already exists.");
            }

            _repositories.Add(key.ToLower(), connection);
        }

        /// <summary>
        /// Retrieves a repository from the collection by its key.
        /// </summary>
        /// <param name="key">The key associated with the repository.</param>
        /// <returns>The repository associated with the specified key.</returns>
        /// <exception cref="RepositoryNotFoundException">Thrown when no repository with the specified key is found.</exception>
        public IRepositoryAsync Get(string key)
        {
            if (!_repositories.ContainsKey(key))
            {
                throw new RepositoryNotFoundException($"Connection with key {key} not found.");
            }

            return _repositories[key];
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<IRepositoryAsync> GetEnumerator()
        {
            return _repositories.Values.GetEnumerator();
        }

        /// <summary>
        /// Removes a repository from the collection by its key.
        /// </summary>
        /// <param name="key">The key associated with the repository to remove.</param>
        /// <returns><c>true</c> if the repository with the specified key is removed; otherwise, <c>false</c>.</returns>
        public bool Remove(string key)
        {
            return _repositories.Remove(key);
        }

        /// <summary>
        /// Gets the number of repositories in the collection.
        /// </summary>
        public int Count => _repositories.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _repositories.GetEnumerator();
        }

        /// <summary>
        /// Determines whether the collection contains a repository with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the collection.</param>
        /// <returns><c>true</c> if the collection contains a repository with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return _repositories.ContainsKey(key);
        }
    }
}
