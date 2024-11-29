using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Data.Common;

namespace SqlExecute.Engine.Repositories
{
    /// <summary>
    /// Factory class for creating repository instances using registered builder strategies.
    /// </summary>
    /// <example>
    /// <code>
    /// using SqlExecute.Engine.Repositories;
    /// using SqlExecute.Engine.Repositories.Abstractions;
    /// using System.Data.Common;
    /// 
    /// // Build a Repository class using the IRepositoryAsync interface
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
    /// // For the repository that was built, implement a way to build the new repository
    /// // using the connection string.
    /// public class SqlRepositoryBuilderStrategy : IRepositoryBuilderStrategy
    /// {
    ///     public IRepositoryAsync Build(string connectionString)
    ///     {
    ///         // Implementation for building a SQL repository
    ///         return new SqlRepository(connectionString);
    ///     }
    /// }
    /// 
    /// public class RepositoryFactoryExample
    /// {
    ///     public void RunExample()
    ///     {
    ///         // Create a new RepositoryFactory
    ///         var factory = new RepositoryFactory();
    ///         // Register the SqlRepositoryBuilderStrategy class with a 
    ///         // key of "sql"
    ///         factory.Register("sql", new SqlRepositoryBuilderStrategy());
    /// 
    ///         // Use the connections string provided, to build the SqlRepository
    ///         // as an IRepositoryAsync
    ///         var repository = factory.Build("sql", "your-connection-string");
    /// 
    ///         // Use the repository
    ///     }
    /// }
    /// </code>
    /// </example>
    public class RepositoryFactory
    {
        private readonly Dictionary<string, IRepositoryBuilderStrategy> _strategies = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Registers a repository builder strategy with a specified key.
        /// </summary>
        /// <param name="key">The key to associate with the builder strategy.</param>
        /// <param name="strategy">The builder strategy to register.</param>
        /// <exception cref="RepositoryAlreadyExistsException">Thrown when a strategy with the specified key already exists.</exception>
        public void Register(string key, IRepositoryBuilderStrategy strategy)
        {
            if (_strategies.ContainsKey(key))
            {
                throw new RepositoryAlreadyExistsException($"Connection builder strategy with key {key} already exists.");
            }

            _strategies.Add(key, strategy);
        }

        /// <summary>
        /// Builds a repository instance using the specified key and connection string.
        /// </summary>
        /// <param name="key">The key associated with the builder strategy.</param>
        /// <param name="connectionString">The connection string to use for the repository.</param>
        /// <returns>An instance of <see cref="IRepositoryAsync"/>.</returns>
        /// <exception cref="RepositoryNotFoundException">Thrown when no strategy with the specified key is found.</exception>
        public IRepositoryAsync Build(string key, string connectionString)
        {
            if (!_strategies.ContainsKey(key))
            {
                throw new RepositoryNotFoundException($"Connection builder strategy with key {key} not found.");
            }

            return _strategies[key].Build(connectionString);
        }
    }
}
