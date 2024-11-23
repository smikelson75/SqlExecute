using FluentValidation;
using ValidationException = SqlExecute.Engine.Exceptions.ValidationException;

namespace SqlExecute.Engine
{
    public static class Validator

    {
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
