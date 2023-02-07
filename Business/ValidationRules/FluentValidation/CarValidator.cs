using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(3);
        }
    }
}
