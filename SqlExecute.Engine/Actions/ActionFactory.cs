using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Exceptions;

namespace SqlExecute.Engine.Actions
{
    /// <summary>
    /// Factory class for creating actions based on the provided type.
    /// </summary>
    /// <example>
    /// <code>
    /// using SqlExecute.Engine.Repositories;
    /// using SqlExecute.Engine.Sqlite;
    /// using SqlExecute.Storage.Yaml;
    /// using SqlExecute.Storage.Yaml.Models;
    /// 
    /// var collection = new RepositoryCollection()
    /// 
    /// // Get Configuration items, including Action and Connections
    /// var configuration = ProcessConfiguration.GetConfiguration("config.yaml");
    /// 
    /// // Create and register Repository Builders for create new Repositorys
    /// var builder = new RepositoryBuilder()
    /// builder.Register("sqlite", new SqliteRepositoryBuilder());
    /// 
    /// // For each connection in the configuration
    /// foreach (var connection in Configuration.Connections)
    /// {
    ///     // Build and add the connection to the Collection
    ///     collection.Add(connection.Name, builder.Build(connection.Provider), connection.ConnectionString));
    /// }
    /// 
    /// // Build the ActionFactory and Register any ActionBuilders
    /// var actionFactory = new ActionFactory();
    /// actionFactory.Register("NonQuery", new NonQueryBuilder(collection));
    /// 
    /// // Here are the parameters required for NonQueryAction
    /// var parameters = new Dictionary<string, object>
    /// {
    ///     { "connection", "local" },
    ///     { "queries", new List<object> { "INSERT INTO dbo.Users VALUES (1, \"John Doe\")", "INSERT INTO dbo.Users VALUES (2, \"Jane Doe\")" } }
    /// };
    /// 
    /// // Get the new Action.
    /// var action = actionFactory.Create("MyAction", "nonquery", parameters);
    /// 
    /// // Execute the Action
    /// await action.ExecuteAsync();
    /// </code>
    /// </example>
    public class ActionFactory
    {
        /// <summary>
        /// Gets the dictionary of action builder strategies.
        /// </summary>
        public Dictionary<string, IActionBuilder> ActionBuilders { get; } = new Dictionary<string, IActionBuilder>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Registers a new action builder with the specified name.
        /// </summary>
        /// <param name="name">The name of the action builder.</param>
        /// <param name="builder">The action builder instance.</param>
        /// <exception cref="ArgumentException">Thrown when the name is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the builder is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a builder with the same name already exists.</exception>
        public void Register(string name, IActionBuilder builder)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(builder);

            if (ActionBuilders.ContainsKey(name))
                throw new InvalidOperationException($"{name} is a duplicate builder name being added.");

            ActionBuilders.Add(name, builder);
        }

        /// <summary>
        /// Creates an action based on the specified name, type, and parameters.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="type">The type of the action.</param>
        /// <param name="parameters">The parameters for the action.</param>
        /// <returns>The created action.</returns>
        /// <exception cref="ArgumentException">Thrown when the name or type is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the parameters are null.</exception>
        /// <exception cref="ActionNotFoundException">Thrown when the specified action type is not found.</exception>
        public IAction Create(string name, string type, IDictionary<string, object> parameters)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(type);
            ArgumentNullException.ThrowIfNull(parameters);

            if (!ActionBuilders.TryGetValue(type, out var builder))
            {
                throw new ActionNotFoundException(type);
            }

            var action = builder.SetName(name)
                .AddParametersRange(parameters)
                .Build();

            builder.Reset();
            return action;
        }
    }
}
