using System.Data.Common;

namespace SqlExecute.Engine.Repositories.Abstractions
{
    public interface IRepositoryAsync : IDisposable
    {
        DbConnection Connection { get; }

        Task OpenAsync();

        Task CloseAsync();

        Task<int> ExecuteNonQueryAsync(string query);
    }
}
