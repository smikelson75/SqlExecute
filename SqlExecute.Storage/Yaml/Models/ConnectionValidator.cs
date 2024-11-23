using FluentValidation;


namespace SqlExecute.Storage.Yaml.Models
{
    /// <summary>
    /// Validator for the <see cref="Connection"/> class.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <see cref="ConnectionValidator"/> class:
    /// <code>
    /// var connection = new Connection
    /// {
    ///     Name = "MyConnection",
    ///     ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
    /// };
    /// var validator = new ConnectionValidator();
    /// var result = validator.Validate(connection);
    /// if (result.IsValid)
    /// {
    ///     Console.WriteLine("Connection is valid.");
    /// }
    /// else
    /// {
    ///     Console.WriteLine("Connection is not valid: " + result.Errors[0].ErrorMessage);
    /// }
    /// </code>
    /// </example>
    internal class ConnectionValidator : AbstractValidator<Connection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionValidator"/> class.
        /// </summary>
        public ConnectionValidator()
        {
            RuleFor(connection => connection.Name)
                .NotEmpty()
                .WithMessage("A connection name is required.");

            RuleFor(connection => connection.ConnectionString)
                .NotEmpty()
                .WithMessage("A connection string is required.");
        }
    }
}
