using FluentValidation;
using ValidationException = SqlExecute.Engine.Exceptions.ValidationException;

namespace SqlExecute.Engine
{
    /// <summary>
    /// Provides validation methods for objects using FluentValidation.
    /// </summary>
    /// <example>
    /// <code>
    /// using FluentValidation;
    /// using SqlExecute.Engine;
    /// using SqlExecute.Engine.Exceptions;
    /// 
    /// namespace SqlExecute.Engine.Examples;
    /// 
    /// // Define a sample class to be validated
    /// public class User
    /// {
    ///     public string Name { get; set; }
    ///     public int Age { get; set; }
    /// }
    /// 
    /// // Define a validator for the User class
    /// public class UserValidator : AbstractValidator<User>
    /// {
    ///     public UserValidator()
    ///     {
    ///         RuleFor(user => user.Name).NotEmpty().WithMessage("Name must not be empty.");
    ///         RuleFor(user => user.Age).GreaterThan(0).WithMessage("Age must be greater than 0.");
    ///     }
    /// }

    /// public class ValidatorExample
    /// {
    ///     public void RunValidation()
    ///     {
    ///         var user = new User { Name = "", Age = -1 };
    ///         
    ///         try
    ///         {
    ///             // Validate the user object using the UserValidator
    ///             Validator.Validate<UserValidator, User>(user);
    ///         }
    ///         catch (ValidationException ex)
    ///         {
    ///             Console.WriteLine($"Validation failed: {ex.Message}");
    ///         }
    ///      }
    /// }
    /// </code>
    /// </example>
    public static class Validator

    {
        /// <summary>
        /// Validates the specified object using the specified validator.
        /// </summary>
        /// <typeparam name="TValidator">The type of the validator to use. Must inherit from <see cref="AbstractValidator{TObject}"/>.</typeparam>
        /// <typeparam name="TObject">The type of the object to validate.</typeparam>
        /// <param name="obj">The object to validate.</param>
        /// <exception cref="ValidationException">Thrown when the validation fails.</exception>
        public static void Validate<TValidator, TObject>(TObject obj) 
            where TValidator : AbstractValidator<TObject>, new()
            where TObject : class
        {
            var validator = new TValidator();
            var result = validator.Validate(obj);

            if (!result.IsValid)
            {
                throw new ValidationException(typeof(TObject));
            }
        }
    }
}
