namespace SqlExecute.Engine.Actions.Abstractions
{
    public interface IActionBuilderStrategy
    {
        void SetName(string name);
        void SetType(string type);
        void SetParameters(Dictionary<string, object?> parameters);

        IAction Build();
    }
}
