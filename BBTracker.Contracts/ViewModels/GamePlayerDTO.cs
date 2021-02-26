using FluentValidation;
using System;

namespace BBTracker.Contracts.ViewModels
{
    public class GamePlayerDTO
    {
        public Guid Id { get; set; }
        public bool TeamB { get; set; }
        public bool OnCourt { get; set; }
    }
    public class GamePlayerDTOValidator : AbstractValidator<GamePlayerDTO>
    {
        public GamePlayerDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.TeamB).NotEmpty();
            RuleFor(x => x.OnCourt).NotEmpty();
        }
    }
}
