namespace SqlExecute.Engine.Exceptions
{
    public class RepositoryAlreadyExistsException(string key) : Exception($"Connection \"{key}\" already exists.")
    {
    }
}