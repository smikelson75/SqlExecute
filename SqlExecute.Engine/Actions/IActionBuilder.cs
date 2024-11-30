using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Engine.Actions
{
    public interface IActionBuilder
    {
        IActionBuilder SetName(string name);
        IActionBuilder AddParameter(string name, object value);
        IActionBuilder AddParametersRange(IDictionary<string, object> parameters);
        IAction Build();
        void Reset();
    }
}
