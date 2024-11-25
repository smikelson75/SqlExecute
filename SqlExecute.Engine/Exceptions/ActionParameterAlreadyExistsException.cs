namespace SqlExecute.Engine.Exceptions
{
    public class ActionParameterAlreadyExistsException(string key) : Exception($"Parameter \"{key}\" already exists.")
    {
    }
}
