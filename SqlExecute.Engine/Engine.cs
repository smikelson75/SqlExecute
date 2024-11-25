using System.Data.Common;
using SqlExecute.Engine.Repositories;

namespace SqlExecute.Engine
{
    public class Engine(RepositoryCollection connections)
    {
        private readonly RepositoryCollection _connections = connections;


    }
}
