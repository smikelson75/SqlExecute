using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;
using System.Data.Common;

namespace SqlExecute.Tests.Engine.Core.Repositories
{
    public class RepositoryCollectionTestFixture
    {
        public SqlExecute.Engine.Repositories.RepositoryCollection Collection { get; private set; }
        public IRepositoryAsync Repository { get; private set; }

        public RepositoryCollectionTestFixture()
        {
            Repository = new MockRepository();
            Collection = [];
        }
    }

    internal class MockRepository : IRepositoryAsync
    {
        private bool _disposed = false;

        public DbConnection Connection => throw new NotImplementedException();

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }

                // Dispose unmanaged resources here.

                _disposed = true;
            }
        }

        ~MockRepository()
        {
            Dispose(false);
        }

        public Task<int> ExecuteNonQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task OpenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
