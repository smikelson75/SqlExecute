using FluentValidation;

namespace SqlExecute.Storage.Yaml.Models
{
    /// <summary>
    /// Validator for the <see cref="Configuration"/> class.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <see cref="ConfigurationValidator"/> class:
    /// <code>
    /// var configuration = new Configuration
    /// {
    ///     Version = "1.0.0",
    ///     Connections = new[]
    ///     {
    ///         new Connection { Name = "MyConnection", ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;" }
    ///     },
    ///     Actions = new[]
    ///     {
    ///         new Action { Type = "Execute", Name = "MyAction", Parameters = new Dictionary<string, object> { { "connection", "MyConnection" } } }
    ///     }
    /// };
    /// var validator = new ConfigurationValidator();
    /// var result = validator.Validate(configuration);
    /// if (result.IsValid)
    /// {
    ///     Console.WriteLine("Configuration is valid.");
    /// }
    /// else
    /// {
    ///     Console.WriteLine("Configuration is not valid: " + result.Errors[0].ErrorMessage);
    /// }
    /// </code>
    /// </example>
    internal class ConfigurationValidator : AbstractValidator<Configuration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationValidator"/> class.
        /// </summary>
        public ConfigurationValidator()
        {
            RuleFor(configuration => configuration.Version)
                .NotEmpty()
                .WithMessage("A configuration version is required.");
            RuleFor(configuration => configuration.Version)
                .Matches(@"^1\.0\.0$")
                .WithMessage("The configuration version must be 1.0.0.");

            RuleFor(configuration => configuration.Actions)
                .NotEmpty()
                .WithMessage("At least one action is required.");

            RuleForEach(configuration => configuration.Actions)
                .SetValidator(new ActionValidator());
        }
    }
}
