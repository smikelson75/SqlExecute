using System.Data.Common;

namespace SqlExecute.Engine.Repositories.Abstractions
{
    /// <summary>
    /// Defines the contract for an asynchronous repository that manages database connections and operations.
    /// </summary>
    public interface IRepositoryAsync : IDisposable
    {
        /// <summary>
        /// Gets the database connection associated with the repository.
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Opens the database connection asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task OpenAsync();

        /// <summary>
        /// Closes the database connection asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CloseAsync();

        /// <summary>
        /// Executes a non-query SQL command asynchronously.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A task that represents the asynchronous operation, containing the number of rows affected.</returns>
        Task<int> ExecuteNonQueryAsync(params string[] queries);
    }
}
