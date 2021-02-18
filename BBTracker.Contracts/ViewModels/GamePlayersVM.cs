using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class GamePlayersVM
    {
        public ICollection<GamePlayerDTO> Players { get; set; }
    }
    public class GamePlayerDTO
    {
        public Guid Id { get; set; }
        public bool TeamB { get; set; }
        public bool OnCourt { get; set; }
    }
    public class NewGamePlayersVMValidator : AbstractValidator<GamePlayersVM>
    {
        public NewGamePlayersVMValidator()
        {
            RuleFor(x => x.Players).NotEmpty();
        }
    }
}
