namespace SqlExecute.Engine.Repositories.Abstractions
{
    public interface IRepositoryBuilderStrategy
    {
        IRepositoryAsync Build(string connectionString);
    }
}
