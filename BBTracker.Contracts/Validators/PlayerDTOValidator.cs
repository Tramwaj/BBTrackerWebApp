using FluentValidation;

namespace BBTracker.Contracts.ViewModels
{
    public class PlayerDTOValidator : AbstractValidator<FullPlayerDTO>
    {
        public PlayerDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 20);
            RuleFor(x => x.Nick).NotEmpty().Length(2, 30);
            RuleFor(x => x.City).NotEmpty().Length(2, 30);
        }
    }
}
