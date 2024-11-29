using FluentValidation;

namespace SqlExecute.Storage.Yaml.Models.Validators
{
    /// <summary>
    /// Validator for the <see cref="Action"/> class.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <see cref="ActionValidator"/> class:
    /// <code>
    /// var action = new Action
    /// {
    ///     Type = "Execute",
    ///     Name = "MyAction",
    ///     Parameters = new Dictionary<string, object> { { "connection", "MyConnection" } }
    /// };
    /// var validator = new ActionValidator();
    /// var result = validator.Validate(action);
    /// if (result.IsValid)
    /// {
    ///     Console.WriteLine("Action is valid.");
    /// }
    /// else
    /// {
    ///     Console.WriteLine("Action is not valid: " + result.Errors[0].ErrorMessage);
    /// }
    /// </code>
    /// </example>
    internal class ActionValidator : AbstractValidator<Action>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionValidator"/> class.
        /// </summary>
        public ActionValidator()
        {
            RuleFor(action => action.Type)
                .NotEmpty()
                .WithMessage("The action type is required.");

            RuleFor(action => action.Name)
                .NotEmpty()
                .WithMessage("An action name is required.");

            RuleFor(action => action.Parameters)
                .NotEmpty()
                .WithMessage("An action parameter is required.");

            RuleFor(action => action.Parameters)
                .Must(parameters => parameters.ContainsKey("connection"))
                .WithMessage("Parameters must contain a 'connection' key.");
        }
    }
}
