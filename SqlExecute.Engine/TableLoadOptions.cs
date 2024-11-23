namespace SqlExecute.Engine
{
    public class TableLoadOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Connection { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string FieldTerminator { get; set; } = ",";
        public string LineTerminator { get; set; } = "\n";
        public int MaxBatchSize { get; set; } = -1;
        public int SkipInitialRows { get; set; } = 0;
    }
}
