using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Common;
using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace SqlExecute.Engine.Actions.Impementations
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
        protected NonQueryAction(Guid id, string name, IRepositoryAsync repository, params string[] queries)
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
        protected NonQueryAction(string name, IRepositoryAsync repository, params string[] queries) : this(Guid.NewGuid(), name, repository, queries)
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

        /// <summary>
        /// Creates a new instance of the <see cref="NonQueryAction"/> class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="parameters">The parameters for the action.</param>
        /// <param name="repository">A function to get the repository based on the connection string.</param>
        /// <returns>A new instance of the <see cref="NonQueryAction"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing or invalid.</exception>
        public static NonQueryAction Create(
            string name,
            ActionParameters parameters,
            Func<string, IRepositoryAsync> repository)
        {
            if (!parameters.ContainsKey("connection", typeof(string)))
                throw new ArgumentException("NonQuery action requires a connection of string type");

            if (!parameters.ContainsKey("queries", typeof(List<string>)))
                throw new ArgumentException("NonQuery action requires at least one query");

            List<string> queries = GetQueryList(parameters);

            return new NonQueryAction(
                name,
                repository(parameters.GetParameter<string>("connection")),
                [.. queries]);
        }

        private static List<string> GetQueryList(ActionParameters parameters)
        {
            var queryObjects = parameters.GetParameter<List<object>>("queries");
            var queries = new List<string>();
            queryObjects.ForEach((query) =>
            {
                if (query is not string value)
                {
                    throw new ActionParameterInvalidRequestTypeException(typeof(string), query.GetType());
                }

                queries.Add(value);
            });
            return queries;
        }
    }
}
