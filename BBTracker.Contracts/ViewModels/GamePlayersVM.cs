using FluentValidation;
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
    public class NewGamePlayersVMValidator : AbstractValidator<GamePlayersVM>
    {
        public NewGamePlayersVMValidator()
        {
            RuleFor(x => x.Players).NotEmpty();
        }
    }
}
