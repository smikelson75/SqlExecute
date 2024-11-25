namespace SqlExecute.Engine.Exceptions
{
    public class ActionParameterNotFoundException(string key) : Exception($"Unable to find parameter \"{ key }\".")
    {
    }
}
