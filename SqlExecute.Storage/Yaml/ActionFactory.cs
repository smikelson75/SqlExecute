using SqlExecute.Engine;
using Action = SqlExecute.Storage.Yaml.Models.Action;

namespace SqlExecute.Storage.Yaml
{
    public class ActionFactory<TAction> where TAction : class
    {
        public TAction GetActionOptions(Action action)
        {
            throw new NotImplementedException();
            //return action.Name switch
            //{
            //    "NonQuery" => Get(action.Parameters),
            //}
        }

        public NonQueryOptions Get(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
            //return new NonQueryOptions
            //{
            //    Query = parameters["query"].ToString()
            //};
        }
    }
}
