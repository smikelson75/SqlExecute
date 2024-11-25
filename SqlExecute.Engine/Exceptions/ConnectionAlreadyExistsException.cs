namespace SqlExecute.Engine.Exceptions
{
    public class ConnectionAlreadyExistsException(string key) : Exception($"Connection \"{key}\" already exists.")
    {
    }
}