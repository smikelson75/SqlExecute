using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Engine.Actions.Implementations
{
    public class NonQueryActionBuilder(RepositoryCollection Collection) : IActionBuilder
    {
        public string Name { get; private set; } = string.Empty;
        public string[] Queries { get; private set; } = [];
        public ActionParameters Parameters { get; private set; } = [];

        public IActionBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public IActionBuilder AddParameter(string name, object value)
        {
            Parameters.AddParameter(name, value);
            return this;
        }

        public IActionBuilder AddParametersRange(IDictionary<string, object> parameters)
        {
            Parameters.AddParameterRange(parameters);
            return this;
        }

        private IRepositoryAsync GetRepository()
        {
            if (!Parameters.ContainsKey("connection", typeof(string)))
                throw new ArgumentException("NonQuery action requires a connection of string type");

            var connectionName = Parameters.GetParameter<string>("connection");
            return Collection.Get(connectionName);
        }

        private string[] GetQueries()
        {
            if (!Parameters.ContainsKey("queries", typeof(List<string>)))
                throw new ArgumentException("NonQuery action requires at least one query");

            return [.. Parameters.GetParameter<List<string>>("queries")];
        }

        public IAction Build()
        {
            var queries = GetQueries();
            var repository = GetRepository();
            return new NonQueryAction(Name, repository, queries);
        }

        public void Reset()
        {
            Name = string.Empty;
            Parameters.Clear();
            Queries = [];
        }
    }
}
