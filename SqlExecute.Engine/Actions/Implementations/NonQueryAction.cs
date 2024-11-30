using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Common;
using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SqlExecute.Engine.Actions.Implementations
{
    /// <summary>
    /// Represents an action that executes non-query SQL commands.
    /// </summary>
    /// <remarks>
    /// This class is used to execute SQL commands that do not return any data, such as INSERT, UPDATE, or DELETE statements.
    /// It encapsulates the logic for executing these commands asynchronously and provides a way to track the status of the action.
    /// </remarks>
    /// <example>
    /// <code>
    /// var parameters = new ActionParameters();
    /// parameters.AddParameter("connection", "MyConnectionString");
    /// parameters.AddParameter("queries", new List<string> { "INSERT INTO MyTable (Column1) VALUES ('Value1')" });
    /// 
    /// var action = NonQueryAction.Create("MyNonQueryAction", parameters, connectionString => new MyRepository(connectionString));
    /// var result = await action.ExecuteAsync();
    /// Console.WriteLine($"Rows affected: {result}");
    /// </code>
    /// </example>
    public class NonQueryAction : IAction
    {
        private readonly Entity<Guid> _identity;

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the repository used to execute the queries.
        /// </summary>
        public IRepositoryAsync Repository { get; }

        /// <summary>
        /// Gets the SQL queries to be executed.
        /// </summary>
        public string[] Queries { get; }

        /// <summary>
        /// Gets the current status of the action.
        /// </summary>
        public ActionStatus Status { get; private set; } = ActionStatus.Pending;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonQueryAction"/> class with the specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the action.</param>
        /// <param name="name">The name of the action.</param>
        /// <param name="repository">The repository used to execute the queries.</param>
        /// <param name="queries">The SQL queries to be executed.</param>
        internal NonQueryAction(Guid id, string name, IRepositoryAsync repository, params string[] queries)
        {
            _identity = new Entity<Guid>(id);
            Name = name;
            Repository = repository;
            Queries = queries;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonQueryAction"/> class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="repository">The repository used to execute the queries.</param>
        /// <param name="queries">The SQL queries to be executed.</param>
        internal NonQueryAction(string name, IRepositoryAsync repository, params string[] queries) : this(Guid.NewGuid(), name, repository, queries)
        { }

        /// <summary>
        /// Gets the unique identifier for the action.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _identity.Id;
            }
        }

        /// <summary>
        /// Executes the SQL queries asynchronously.
        /// </summary>
        /// <returns>The total number of rows affected by the queries.</returns>
        public async Task<int> ExecuteAsync()
        {
            Status = ActionStatus.Started;
            var result = 0;
            foreach (var query in Queries)
            {
                result += await Repository.ExecuteNonQueryAsync(query);
            }

            Status = ActionStatus.Complete;
            return result;
        }
    }
}
