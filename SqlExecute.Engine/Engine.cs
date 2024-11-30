using System.Data.Common;
using System.Reflection;
using SqlExecute.Engine.Actions;
using SqlExecute.Engine.Repositories;

namespace SqlExecute.Engine
{
    public class Engine(RepositoryFactory factory)
    {
        public ActionCollection Actions { get; } = [];


        public static Engine Create(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
