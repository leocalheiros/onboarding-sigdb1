using FluentValidation;
using FluentValidation.Results;

namespace OnboardingSIGDB1.Domain.Entities.Common;

public abstract class BaseEntity
{
    public long Id { get; private set; }
    // public bool Valid { get; private set; }
    // public bool Invalid => !Valid;
    // public ValidationResult ValidationResult { get; private set; }
    //
    // public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    // {
    //     ValidationResult = validator.Validate(model);
    //     return Valid = ValidationResult.IsValid;
    // }
}