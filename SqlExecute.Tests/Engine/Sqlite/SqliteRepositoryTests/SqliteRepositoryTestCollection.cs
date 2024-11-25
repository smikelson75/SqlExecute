using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Tests.Engine.Sqlite.SqliteRepositoryTests
{
    [CollectionDefinition("SqliteRepositoryTests")]
    public class SqliteRepositoryTestCollection : ICollectionFixture<SqliteRepositoryTestFixture>
    { }
}
