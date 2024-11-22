using SqlExecute.Storage.Yaml;

var configuration = ProcessConfiguration.GetConfiguration("config.yaml");

Console.WriteLine($"Configuration version: {configuration.Version}");
