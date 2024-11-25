using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Engine.Actions.Impementations
{
    public class NonQueryAction : IAction
    {
        public string Name { get; } 
        public IRepositoryAsync Repository { get; }
        public string[] Queries { get; }

        protected NonQueryAction(string name, IRepositoryAsync repository, params string[] queries)
        {
            Name = name;
            Repository = repository;
            Queries = queries;
        }

        public async Task<int> ExecuteAsync()
        {
            var result = 0;
            foreach (var query in Queries)
            {
                result += await Repository.ExecuteNonQueryAsync(query);
            }
            return result;
        }

        public static NonQueryAction Create(
            string name, 
            ActionParameters parameters, 
            Func<string, IRepositoryAsync> repository)
        {
            if (parameters.ContainsKey("connection", typeof(string)))
                throw new ArgumentException("NonQuery action a connection of string type");

            if (parameters.ContainsKey("queries", typeof(List<string>)))
                throw new ArgumentException("NonQuery action requires at least one query");

            return new NonQueryAction(
                name, 
                repository(parameters.GetParameter<string>("connection")),
                [.. parameters.GetParameter<List<string>>("queries")]);
        }
    }
}
