using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class NewGamePlayersVM
    {
        public ICollection<NewGamePlayerDTO> Players { get; set; }
    }
    public class NewGamePlayerDTO
    {
        public Guid Id { get; set; }
        public bool TeamB { get; set; }
    }
    public class NewGamePlayersVMValidator : AbstractValidator<NewGamePlayersVM>
    {
        public NewGamePlayersVMValidator()
        {
            RuleFor(x => x.Players).NotEmpty();
        }
    }
}
